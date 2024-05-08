jQuery(function ($) {
    var sampleData = initiateDemoData();//see below


    $('#tree1').ace_tree({
        dataSource: sampleData['dataSource1'],
        multiSelect: true,
        cacheItems: true,
        'open-icon': 'ace-icon tree-minus',
        'close-icon': 'ace-icon tree-plus',
        'itemSelect': true,
        'folderSelect': false,
        'selected-icon': 'ace-icon fa fa-check',
        'unselected-icon': 'ace-icon fa fa-times',
        loadingHTML: '<div class="tree-loading"><i class="ace-icon fa fa-refresh fa-spin blue"></i></div>'
    });

    $('#tree2').ace_tree({
        dataSource: sampleData['dataSource2'],
        loadingHTML: '<div class="tree-loading"><i class="ace-icon fa fa-refresh fa-spin blue"></i></div>',
        'open-icon': 'ace-icon fa fa-folder-open',
        'close-icon': 'ace-icon fa fa-folder',
        'itemSelect': true,
        'folderSelect': true,
        'multiSelect': true,
        'selected-icon': null,
        'unselected-icon': null,
        'folder-open-icon': 'ace-icon tree-plus',
        'folder-close-icon': 'ace-icon tree-minus'
    });


    /**
	//Use something like this to reload data	
	$('#tree1').find("li:not([data-template])").remove();
	$('#tree1').tree('render');
	*/


    /**
	//please refer to docs for more info
	$('#tree1')
	.on('loaded.fu.tree', function(e) {
	})
	.on('updated.fu.tree', function(e, result) {
	})
	.on('selected.fu.tree', function(e) {
	})
	.on('deselected.fu.tree', function(e) {
	})
	.on('opened.fu.tree', function(e) {
	})
	.on('closed.fu.tree', function(e) {
	});
	*/


    function initiateDemoData() {
        var tree_data = {
            'dashboard': { text: 'Dashboard', type: 'folder' },
            'admin': { text: 'Admin', type: 'folder' },
            'purchase': { text: 'Purchase', type: 'folder' },
            'stores': { text: 'Stores', type: 'folder' },
            'accounts': { text: 'Accounts', type: 'folder' }
        }
        tree_data['dashboard']['additionalParameters'] = {
            'children': {
                'calendarevents': { text: 'Calendar & Events', type: 'item' },
                'noticeboard': { text: 'Notice Board', type: 'item' },
                'notification': { text: 'Notification', type: 'item' }
            }
        }
        tree_data['admin']['additionalParameters'] = {
            'children': {
                'employeemaster': { text: 'Employee Master', type: 'item' },
                'itemmaster': { text: 'Item Master', type: 'item' },
                'ratehistorymaster': { text: 'Rate History Master', type: 'item' },
                'vendormaster': { text: 'Vendor Master', type: 'item' },
                'taxmaster': { text: 'Tax Master', type: 'item' },
                'grndetails': { text: 'GRN Details', type: 'item' },
                'rolecreation': { text: 'Role Creation', type: 'item' },
                'rolemappingdetails': { text: 'Role Mapping Details', type: 'item' },
                'calendarevents': { text: 'Calendar Events', type: 'item' },
                'noticeboard': { text: 'Notice Board', type: 'item' },
                'notification': { text: 'Notification', type: 'item' }
            }
        }
        //tree_data['vehicles']['additionalParameters']['children']['cars']['additionalParameters'] = {
        //    'children': {
        //        'classics': { text: 'Classics', type: 'item' },
        //        'convertibles': { text: 'Convertibles', type: 'item' },
        //        'coupes': { text: 'Coupes', type: 'item' },
        //        'hatchbacks': { text: 'Hatchbacks', type: 'item' },
        //        'hybrids': { text: 'Hybrids', type: 'item' },
        //        'suvs': { text: 'SUVs', type: 'item' },
        //        'sedans': { text: 'Sedans', type: 'item' },
        //        'trucks': { text: 'Trucks', type: 'item' }
        //    }
        //}
        tree_data['purchase']['additionalParameters'] = {
            'children': {
                'viewsupplierdetails': { text: 'View Supplier Details', type: 'item' },
                'downloadmaterialdrawings': { text: 'Download Material Drawings', type: 'item' },
                'viewratehistory': { text: 'View Rate History', type: 'item' },
                'downloadpurchaseorder': { text: 'Download Purchase Order', type: 'item' },
                'downloadpurchaseschedules': { text: 'Download Purchase Schedules', type: 'item' }
            }
        }
        tree_data['stores']['additionalParameters'] = {
            'children': {
                'createuploadasn': { text: 'Create / Upload ASN (Advance Shipment Notice)', type: 'item' },
                'updatesupplierstock': { text: 'Update Supplier Stock', type: 'item' },
                'updatesuppliershopshortage': { text: 'Update Supplier Shop Shortage', type: 'item' },
                'viewrejectiondetails': { text: 'View Rejection Details', type: 'item' },
                'viewgrnstatus': { text: 'View GRN Status', type: 'item' },
                'viewdeliveryperformance': { text: 'View Delivery Performance', type: 'item' }
            }
        }
        //tree_data['real-estate']['additionalParameters'] = {
        //    'children': {
        //        'apartments': { text: 'Apartments', type: 'item' },
        //        'villas': { text: 'Villas', type: 'item' },
        //        'plots': { text: 'Plots', type: 'item' }
        //    }
        //}
        tree_data['accounts']['additionalParameters'] = {
            'children': {
                'viewpaymentstatus': { text: 'View Payment Status', type: 'item' },
                'viewcreditnote': { text: 'View Credit Note', type: 'item' },
                'viewdebitnote': { text: 'View Debit Note', type: 'item' },
                'viewstatementofaccounts': { text: 'View Statement of Accounts', type: 'item' }
            }
        }

        var dataSource1 = function (options, callback) {
            var $data = null
            if (!("text" in options) && !("type" in options)) {
                $data = tree_data;//the root tree
                callback({ data: $data });
                return;
            }
            else if ("type" in options && options.type == "folder") {
                if ("additionalParameters" in options && "children" in options.additionalParameters)
                    $data = options.additionalParameters.children || {};
                else $data = {}//no data
            }

            if ($data != null)//this setTimeout is only for mimicking some random delay
                setTimeout(function () { callback({ data: $data }); }, parseInt(Math.random() * 500) + 200);

            //we have used static data here
            //but you can retrieve your data dynamically from a server using ajax call
            //checkout examples/treeview.html and examples/treeview.js for more info
        }




        var tree_data_2 = {
            'pictures': { text: 'Pictures', type: 'folder', 'icon-class': 'red' },
            'music': { text: 'Music', type: 'folder', 'icon-class': 'orange' },
            'video': { text: 'Video', type: 'folder', 'icon-class': 'blue' },
            'documents': { text: 'Documents', type: 'folder', 'icon-class': 'green' },
            'backup': { text: 'Backup', type: 'folder' },
            'readme': { text: '<i class="ace-icon fa fa-file-text grey"></i> ReadMe.txt', type: 'item' },
            'manual': { text: '<i class="ace-icon fa fa-book blue"></i> Manual.html', type: 'item' }
        }
        tree_data_2['music']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-music blue"></i> song1.ogg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-music blue"></i> song2.ogg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-music blue"></i> song3.ogg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-music blue"></i> song4.ogg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-music blue"></i> song5.ogg', type: 'item' }
            ]
        }
        tree_data_2['video']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-film blue"></i> movie1.avi', type: 'item' },
				{ text: '<i class="ace-icon fa fa-film blue"></i> movie2.avi', type: 'item' },
				{ text: '<i class="ace-icon fa fa-film blue"></i> movie3.avi', type: 'item' },
				{ text: '<i class="ace-icon fa fa-film blue"></i> movie4.avi', type: 'item' },
				{ text: '<i class="ace-icon fa fa-film blue"></i> movie5.avi', type: 'item' }
            ]
        }
        tree_data_2['pictures']['additionalParameters'] = {
            'children': {
                'wallpapers': { text: 'Wallpapers', type: 'folder', 'icon-class': 'pink' },
                'camera': { text: 'Camera', type: 'folder', 'icon-class': 'pink' }
            }
        }
        tree_data_2['pictures']['additionalParameters']['children']['wallpapers']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> wallpaper1.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> wallpaper2.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> wallpaper3.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> wallpaper4.jpg', type: 'item' }
            ]
        }
        tree_data_2['pictures']['additionalParameters']['children']['camera']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo1.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo2.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo3.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo4.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo5.jpg', type: 'item' },
				{ text: '<i class="ace-icon fa fa-picture-o green"></i> photo6.jpg', type: 'item' }
            ]
        }


        tree_data_2['documents']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-file-text red"></i> document1.pdf', type: 'item' },
				{ text: '<i class="ace-icon fa fa-file-text grey"></i> document2.doc', type: 'item' },
				{ text: '<i class="ace-icon fa fa-file-text grey"></i> document3.doc', type: 'item' },
				{ text: '<i class="ace-icon fa fa-file-text red"></i> document4.pdf', type: 'item' },
				{ text: '<i class="ace-icon fa fa-file-text grey"></i> document5.doc', type: 'item' }
            ]
        }

        tree_data_2['backup']['additionalParameters'] = {
            'children': [
				{ text: '<i class="ace-icon fa fa-archive brown"></i> backup1.zip', type: 'item' },
				{ text: '<i class="ace-icon fa fa-archive brown"></i> backup2.zip', type: 'item' },
				{ text: '<i class="ace-icon fa fa-archive brown"></i> backup3.zip', type: 'item' },
				{ text: '<i class="ace-icon fa fa-archive brown"></i> backup4.zip', type: 'item' }
            ]
        }
        var dataSource2 = function (options, callback) {
            var $data = null
            if (!("text" in options) && !("type" in options)) {
                $data = tree_data_2;//the root tree
                callback({ data: $data });
                return;
            }
            else if ("type" in options && options.type == "folder") {
                if ("additionalParameters" in options && "children" in options.additionalParameters)
                    $data = options.additionalParameters.children || {};
                else $data = {}//no data
            }

            if ($data != null)//this setTimeout is only for mimicking some random delay
                setTimeout(function () { callback({ data: $data }); }, parseInt(Math.random() * 500) + 200);

            //we have used static data here
            //but you can retrieve your data dynamically from a server using ajax call
            //checkout examples/treeview.html and examples/treeview.js for more info
        }


        return { 'dataSource1': dataSource1, 'dataSource2': dataSource2 }
    }

});
