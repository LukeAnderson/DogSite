
//call on load
$(document).ready(function () {
    $.ajax({
        url: '@Url.Action("GetArticle","Test",new { Id = Model.Id})',
        method: 'GET',
        success: function (data) {
            $("#currentArticle").html(data)
        }
    });
});

//Angular commment app
var app = angular.module('CommentApp', []);
app.controller('CommentController', function ($scope) {

    //initialize
    var postCommentPlaceHolderText = "Comment!";
    $scope.postComment = postCommentPlaceHolderText;

    $scope.count = @Model.Id;

    var maxCount;
    $.ajax({
        url: '@Url.Action("GetMaxCount","Test")',
        method: 'GET',
        success: function (data) {
            maxCount = data;
        }
    });

    $scope.clearPlaceHolderText = function () {
        if ($scope.postComment === postCommentPlaceHolderText)
            $scope.postComment = "All clear!"
    }

    $scope.getArticle = function () {
        $.ajax({
            url: '@Url.Action("GetArticle","Test")?Id=' + $scope.count,
            method: 'GET',
            success: function (data) {
                $("#currentArticle").html(data)
            }
        });
    }

    //get next/previous articles
    $scope.getNextArticleId = function () {
        $scope.count = $scope.count >= maxCount ? 1 : ++$scope.count;
        $scope.getArticle();
        $scope.getComments();
    }

    $scope.getPreviousArticleId = function () {
        $scope.count = $scope.count <= 1 ? maxCount : --$scope.count;
        $scope.getArticle();
        $scope.getComments();
    }

    //add comment to article where articleId === $scope.count
    $scope.addComment = function () {
        $scope.comments += "\n\n\n" + $scope.postComment
        $.ajax({
            url: '@Url.Action("AddComment", "Test")?comment=' + $scope.postComment + "&articleId=" + $scope.count,
            method: 'POST',
            success: function (data) {
                $scope.label = data;
                $scope.$apply();
            }
        });
    }

    //get comments for article where articleId === $scope.count
    $scope.getComments = function () {
        $.ajax({
            url: '@Url.Action("GetComments","Test")?articleId=' + $scope.count,
            method: 'GET',
            success: function (data) {
                $scope.comments = data
                $scope.$apply();
            }
        });
    };
    $scope.getComments();//call to get comments on initial article
});