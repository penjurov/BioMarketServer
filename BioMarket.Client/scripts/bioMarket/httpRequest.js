define(['Q'], function (Q) {
	var httpRequest = (function () {
		var getJSON = function(url, contentType, acceptType) {
			var deferred = Q.defer();
			Q.stopUnhandledRejectionTracking();

			$.ajax({
				url: url,
				type: 'GET',
				contentType : contentType || '',
				acceptType : acceptType || '',
				success: function (data) {
					deferred.resolve(data);
				},
				error: function (err) {
					deferred.reject(err);
				}
			});

			return deferred.promise;
		};

		var postJSON = function(url, contentType, acceptType, data, sessionKey) {
			var deferred = Q.defer(),
				type = 'PUT';

			if (data) {
				type = 'POST';
			}

			Q.stopUnhandledRejectionTracking();

			$.ajax({
				url : url,
				type : type,
				method : type,
				dataType : "json",
				ContentType: "application/json; charset=utf-8",
				AcceptType: acceptType,
				data : data,
				success: function (data) {
					deferred.resolve(data);
				},
				error: function (err) {
					deferred.reject(err);
				}
			});

			return deferred.promise;
		};

		return {
			getJSON: getJSON,
			postJSON: postJSON
		};
	}());
	return httpRequest;
});