define(['jquery', 'logic', 'httpRequest', 'handlebars', 'kendo'], function ($,logic, httpRequest) {
	var START_MENU_SIZE = 300;
	var contentType = 'application/json',
		acceptType = 'application/json';

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

	var initUpdateClientPage = function () {
		initPage('#menu', $('#menu-container'));


		$('#main-content').load('updateClient.html', function () {
			$('#client-update-email').kendoMaskedTextBox();
			$('#client-update-password').kendoMaskedTextBox();
			$('#client-repeat-update-password').kendoMaskedTextBox();
			$('#client-update-firstname').kendoMaskedTextBox();
			$('#client-update-lastname').kendoMaskedTextBox();
			$('#client-update-phone').kendoMaskedTextBox();
			$('#client-update-button').kendoButton();
			$('#client-update-email').focus();
		});

		logic.populateClientProfile();

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

	var initUpdateFarmPage = function() {
		initPage('#menu', $('#menu-container'));

		$('#main-content').load('updateFarm.html', function() {
			$('#farm-email').kendoMaskedTextBox();
			$('#farm-username').kendoMaskedTextBox();
			$('#farm-old-password').kendoMaskedTextBox();
			$('#farm-new-password').kendoMaskedTextBox();
			$('#farm-repeat-new-password').kendoMaskedTextBox();
			$('#farm-name').kendoMaskedTextBox();
			$('#farm-address').kendoMaskedTextBox();
			$('#farm-phone').kendoMaskedTextBox();
			$('#farm-owner').kendoMaskedTextBox();
			$('#farm-latitude').kendoMaskedTextBox();
			$('#farm-longitude').kendoMaskedTextBox();
			$('#farm-update-button').kendoButton();
			$('#farm-email').focus();
		});
		
		logic.populateFarmProfile();
	};

	var initAddOfferPage = function() {
		initPage('#menu', $('#menu-container'));
		
		$('#main-content').load('addOffer.html', function() {
			$('#add-offer-products').kendoComboBox();
			$('#add-offer-quantity').kendoMaskedTextBox();
			$('#add-offer-choose-photo-button').kendoButton();
			$('#add-offer-button').kendoButton();
			$('#add-offer-product').focus();
			addProducts();
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

	// Adding products types from JSON array to Kendo multiselect
	var addProducts = function() {
		var url = 'http://localhost:6022/api/Product/All',
			products = [];

		httpRequest.getJSON(url, contentType, acceptType)
			.then(function (data) {
					products = data;
					handleBarConvert($('#product-template'), $('#add-offer-products'), products);
					$("#add-offer-products").kendoComboBox().data("kendoComboBox");
				}, function (err) {
					alert(JSON.parse(err.responseText).message);
				}
			);


	};

	// Handlebar templates
	function handleBarConvert(template, container, items) {
		Handlebars.registerHelper('multiply', function (first, second) {
			var result = first * second;
			return result;
		});

		var currentTemplate = Handlebars.compile(template.html());

		container.html(currentTemplate({
			products : items
		}));
	}

	return {
		initHomePage: initHomePage,
		initLoginPage: initLoginPage,
		initRegisterClientPage: initRegisterClientPage,
		initUpdateClientPage: initUpdateClientPage,
		initRegisterFarmPage: initRegisterFarmPage,
		initUpdateFarmPage: initUpdateFarmPage,
		initAddOfferPage: initAddOfferPage,
		showError: showError,
		drawKendoGrid: drawKendoGrid
	};
});