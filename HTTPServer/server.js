var http = require("http");
var url = require("url");
var fs = require("fs");
 
http.createServer(function(request, response) {
        response.writeHead(200, {"Content-Type": "text/plain"});

        var pathname = url.parse(request.url).pathname;
        var id = request.url.substr(pathname.lastIndexOf('/') + 1);

        if(request.url === '/') {
            fs.readFile("./public/peopleWithoutAge.txt",'utf8', function(error, data){
                if(error){    
                    response.statusCode = 404;
                    response.end("Ресурс не найден!");
                }   
                else{
                    response.end(data);
                }    
            })
        }
        else {
            fs.readFile("./public/peopleWithAge.txt",'utf8', function(error, data){
                if(error){
                    response.statusCode = 404;
                    response.end("Ресурс не найден!");
                }   
                else{
                    response.end(JSON.stringify(JSON.parse(data)[id]));  
                }
            })
        }
        
}).listen(8080);