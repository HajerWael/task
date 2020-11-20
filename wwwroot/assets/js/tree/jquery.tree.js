'use strict';
$( document ).ready(function() {
    // Basic
    $('#basicTree').jstree({
		'core' : {
			'themes' : {
				'responsive': false
			}
		},
        'types' : {
            'default' : {
                'icon' : 'icofont icofont-folder'
            },
            'file' : {
                'icon' : 'icofont icofont-file-alt'
            }
        },
        'plugins' : ['types']
    });
    
  
});