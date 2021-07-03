

function onTest(){
    debugger;
    var http = new XMLHttpRequest();
    http.open("post", "/Home/Index2", false)
    var data = "aaa=" + "沾上干";
    http.setRequestHeader("content-type", "application/x-www-form-urlencoded");

    http.onreadystatechange = function () {
        debugger;
        /* console.log(http.response);  //数据
          console.log(http.readyState);*/  //读物的状态码  0 1 2 3 4
        if (http.readyState == 4) {
            var ret = http.responseText;
            //var ajax1 = JSON.parse(http.responseText);  //json转化字符串
            ////  var ajax1=JSON.parse (http.response);  //json转化字符串
            //console.log(ajax1);
        }
    }
    http.send(data);
};

