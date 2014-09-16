define(['jquery', 'handlebars', 'kendo'], function ($) {
	var START_MENU_SIZE = 300;

	var initHomePage = function() {
		var username = localStorage.getItem('crowdShareUserName');
		
		initPage('#menu', $('#menu-container'));

		$('#main-content').load('home.html', function() {
			if (username && username !=='' && username !=='null') {
				$('#logout').text('Welcome: ' + username + ' (Logout)');
			}
		});
	};

	var initLoginPage = function(chatItems) {
		initPage('#menu', $('#menu-container'));

		$('#main-content').load('login.html', function() {
			$('#login-nickname').kendoMaskedTextBox();
			$('#login-password').kendoMaskedTextBox();
			$('#login-button').kendoButton();
			$('#login-nickname').focus();
		});
	};

	var initRegisterClientPage = function() {
		initPage('#menu', $('#menu-container'));

		$('#main-content').load('registerClient.html', function() {
			$('#client-register-email').kendoMaskedTextBox();
			$('#client-register-username').kendoMaskedTextBox();
			$('#client-register-password').kendoMaskedTextBox();
			$('#client-repeat-register-password').kendoMaskedTextBox();
			$('#client-register-firstname').kendoMaskedTextBox();
			$('#client-register-lastname').kendoMaskedTextBox();
			$('#client-register-phone').kendoMaskedTextBox();
			$('#client-register-button').kendoButton();
			$('#client-register-email').focus();
		});
	};

	var initRegisterFarmPage = function() {
		initPage('#menu', $('#menu-container'));

		$('#main-content').load('registerFarm.html', function() {
			$('#farm-register-email').kendoMaskedTextBox();
			$('#farm-register-username').kendoMaskedTextBox();
			$('#farm-register-password').kendoMaskedTextBox();
			$('#farm-repeat-register-password').kendoMaskedTextBox();
			$('#farm-register-name').kendoMaskedTextBox();
			$('#farm-register-address').kendoMaskedTextBox();
			$('#farm-register-phone').kendoMaskedTextBox();
			$('#farm-register-owner').kendoMaskedTextBox();
			$('#farm-register-latitude').kendoMaskedTextBox();
			$('#farm-register-longitude').kendoMaskedTextBox();
			$('#farm-register-button').kendoButton();
			$('#farm-register-email').focus();
		});
	};

	var showError = function(err) {
		$('#main-content').text(err.responseText);
	};

	var initPage = function (menu, container) {
		container.load('menu.html', function() {
			$(menu).kendoMenu();
		});
		$('#main-content').text(' ');
	};

	function drawKendoGrid(items, postsCount) {
		$('#grid').kendoGrid({
			dataSource: {
				data : items,
				pageSize: postsCount|0 || 10
			},
			height: window.innerHeight - START_MENU_SIZE,
			groupable: true,
			sortable: true,
			filterable: true,
			pageable: {
				refresh: true,
				pageSizes: true,
				buttonCount: 5
			},
			columns:	[	{ field: "user.username", title: "User" },
							{ field: "postDate", title: "Date"},
							{ field: "title", title: "Title"},
							{ field: "body", title: "Body"},
						]
		});

	}

	return {
		initHomePage: initHomePage,
		initLoginPage: initLoginPage,
		initRegisterClientPage: initRegisterClientPage,
		initRegisterFarmPage : initRegisterFarmPage,
		showError: showError,
		drawKendoGrid: drawKendoGrid
	};
});