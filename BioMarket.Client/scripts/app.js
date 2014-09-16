(function () {
	require.config({
		paths: {
			jquery: "libs/jquery.min",
			sammy: "libs/sammy-latest.min",
			handlebars: "libs/handlebars",
			kendo: "libs/kendo.web.min",
			Q: "libs/q.min",
			cryptojs: 'libs/cryptojs',
			sha1: 'libs/sha1',
			underscore: 'libs/underscore',
			httpRequest : "bioMarket/httpRequest",
			logic: "bioMarket/logic",
			ui: "bioMarket/ui",
			events: "bioMarket/events"
		}
	});

	require(["sammy", "ui", "logic", "events"], function (sammy, ui, logic) {
		var app = sammy("#main-content", function() {
			this.get("#/", function () {
				ui.initHomePage();
			});

			this.get("#/login", function () {
				ui.initLoginPage();
			});

			this.get("#/registerClient", function () {
				ui.initRegisterClientPage();
			});

			this.get("#/registerFarm", function () {
				ui.initRegisterFarmPage();
			});

			this.get("#/logout", function () {
				logic.logout();
			});
		});

		app.run("#/");
	});
}());