jQuery.postJSON = function(url, args, successCall, errorCall){
    $.ajax({
        type:"post",
        url:url,
        data:JSON.stringify(args),
        contentType:"application/json; charset=UTF-8",
        success:successCall,
        error:errorCall
    });
}
function PageManager(){
    this.configs = {
        curveOptions:{
            series:{
                lines:{show:true},
                points:{radius:3, show:true}
            },
            xaxes:[{
                mode:"time",
                timeformat:"%H:%M",
                axisLabel:"Time",
            }]
        },
        processes:{
            sort:{//从上到下排序["name", "state", "cpuRate", "memRate", "memResident", "memSize", "pid", "runtime"]
                select:"cpuRate",
                dir:"down"
            },
            selectedPid:null,//选中的进程
        }
    } 
    this.page = "processes";
    this.data = null;
    
    this.init = function(){
        var self = this;
        var sort = this.configs.processes.sort;
        var selectedPid = this.configs.processes.selectedPid;
        $("#processinfo-name")[0].onclick = function(){
            if (sort.select == "name"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "name";
                sort.dir = "up";
            }
            self._sortTable("name", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-state")[0].onclick = function(){
            if (sort.select == "state"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "state";
                sort.dir = "up";
            }
            self._sortTable("state", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-cpuRate")[0].onclick = function(){
            if (sort.select == "cpuRate"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "cpuRate";
                sort.dir = "up";
            }
            self._sortTable("cpuRate", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-memRate")[0].onclick = function(){
            if (sort.select == "memRate"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "memRate";
                sort.dir = "up";
            }
            self._sortTable("memRate", sort.dir, selectedPid);
        };
        $("#processinfo-memResident")[0].onclick = function(){
            if (sort.select == "memResident"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "memResident";
                sort.dir = "up";
            }
            self._sortTable("memResident", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-memSize")[0].onclick = function(){
            if (sort.select == "memSize"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "memSize";
                sort.dir = "up";
            }
            self._sortTable("memSize", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-pid")[0].onclick = function(){
            if (sort.select == "pid"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "pid";
                sort.dir = "up";
            }
            self._sortTable("pid", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };
        $("#processinfo-runtime")[0].onclick = function(){
            if (sort.select == "runtime"){
                if (sort.dir == "up")
                    sort.dir = "down";
                else
                    sort.dir = "up";
            }else{
                sort.select = "runtime";
                sort.dir = "up";
            }
            self._sortTable("runtime", sort.dir, selectedPid);
            $(".processinfo-sort").removeClass("selected");
            $(this).addClass("selected");
        };

    }





    this.paint= function(data){
        this.data = data;
        if (this.page == "processes"){
            this._refreshProcessesPage(data.processes);
        }
        else if (this.page == "analytics"){
            this._refreshAnalyticsPage(data.dynamic);
        }
        else if (this.page == "overview"){
            this._refreshOverviewPage(data.sys);
        }
    }
    this.showPage = function(page){
        if (page != "overview" && page !="processes" && page != "analytics"){
            alert("error!");
            return ;
        }
        $(".dashboard-page").hide();
        if (page == "processes"){
            if (this.data != null){
                this._refreshProcessesPage(this.data.processes);
            }
            $("#processes-page").show();
        }
        else if (page == "analytics"){
            if (this.data != null){
                this._refreshAnalyticsPage(this.data.dynamic);             
            }
            $("#analytics-page").show();
        }
        else if (page == "overview"){
            if (this.data != null){
                this._refreshOverviewPage(this.data.sys);
            }
            $("#overview-page").show();
        }
    }
    this._refreshProcessesPage = function(data){
        $("#cpu-rate-progressbar").width(data.cpuRate+"%");
        $("#cpu-rate-progressbar").text(data.cpuRate+" %");

        $("#mem-rate-progressbar").width(data.memRate+"%");
        $("#mem-rate-progressbar").text(data.memRate+" %");
        
        $("#mem-total").text(this._size_readable(data.meminfo.mem.total, "KB"));
        $("#mem-free").text(this._size_readable(data.meminfo.mem.free, "KB"));
        $("#mem-buffers").text(this._size_readable(data.meminfo.mem.buffers, "KB"));
        $("#mem-cached").text(this._size_readable(data.meminfo.mem.cached, "KB"));

        $("#processes-total").text(data.process.total);
        $("#processes-running").text(data.process.running);
        $("#processes-sleeping").text(data.process.sleeping);
        var sort = this.configs.processes.sort;
        var selectedPid = this.configs.processes.selectedPid.
        this._sortTable(sort.select, sort.dir, selectedPid);
    }
    this._refreshAnalyticsPage = function(data){
        $.plot($("#total-cpu-flot-placeholder"), 
               [{label:"Cpu Performance", data:data.cpu[0].history}],
               this.configs.curveOptions
              );
        /*
        $("#cpu-performance-detail").html("");
        for (var i = 1; i < data.cpu.length; i ++){
            var id_name = "cpu-detail-"+data.cpu[i].processor;
            if ($("#" + id_name).length == 0){
                $("<div class='col-md-{{length}} flot-small-placeholder' id='{{idName}}'></div>".replace(/{{length}}/g, Math.round(12/data.cpu.length)).replace(/{{idName}}/g, id_name)).appendTo("#cpu-performance-detail");
            }
            $.plot($("#"+id_name),
                   [{label:"Cpu Performance", data:data.cpu[i].history}],
                   this.configs.curveOptions
                  );
        }
        */
        $.plot($("#mem-flot-placeholder"), 
               [{label:"Cpu Performance", data:data.memory}],
               this.configs.curveOptions
              );
        
    }
    this._refreshOverviewPage = function(data){
        $("#cpu-name").text(data.cpu.name);
        $("#cpu-cores").text(data.cpu.cores);
        $("#system-version").text(data.version);
        $("#system-more").text(data.text);
    }
    
    this._sortTable = function(head, dir, selectedPid){
        heads = ["name", "state", "cpuRate", "memRate", "memResident", "memSize", "pid", "runtime"]
        if (heads.indexOf(head) == -1 || (dir != "down" && dir != "up"))
            return;
        var processInfoList = this.data.processes.process.processInfo;
        
        processInfoList.sort(function(a, b){
            if (dir == "up"){
                return b[head] - a[head];
            }
            else{
                return a[head] - b[head];
            }
        });
        
        $("#processes-table-tbody").html("");
        for (var i = 0; i < processInfoList.length; i++){
            var process = processInfoList[i]
            $("<tr id='{{pid}}' class='process-info'>\
              <td>{{name}}</td>\
              <td>{{state}}</td>\
              <td>{{cpuRate}}</td>\
              <td>{{memRate}}</td>\
              <td>{{memResident}}</td>\
              <td>{{memSize}}</td>\
              <td>{{pid}}</td>\
              <td>{{runtime}}</td>\
            </tr>".replace(/{{pid}}/g, process["pid"]).replace(/{{name}}/g, process["name"]).replace(/{{state}}/g, process["state"]).replace(/{{cpuRate}}/g, process["cpuRate"]).replace(/{{memRate}}/g, process["memRate"]).replace(/{{memResident}}/g, this._size_readable(process["memResident"], "KB")).replace(/{{memSize}}/g, this._size_readable(process["memSize"], "KB")).replace(/{{runtime}}/g, process["runtime"])
            ).appendTo("#processes-table-tbody");
        }

        adjustTable();
    }
    this._size_readable = function(size, unit){
        var divided_times = 0;
        var last_size = size;
        var step = 1024;
        
        var units = ["B", "KB", "MB", "GB", "TB", "PB"]
        if (units.indexOf(unit) == -1){
            return "size" + unit;
        }
        
        while (true){
            if (Math.floor(last_size / step) > 0){
                divided_times += 1;
                last_size = last_size / step;
            }
            else{
                break;
            }
        }
        last_size = Math.round(last_size*10)/10;
        return String(last_size)+units[units.indexOf(unit)+divided_times]
        
    }
}


function Application(){
    this.pageManager = new PageManager();
    this.pageManager.showPage("processes");
    var self = this;
    $("#nav-processes").click(function(){
        $(this.parentElement.parentElement.children).removeClass("active");

        self.pageManager.showPage("processes");
        $(this.parentElement).addClass("active");
    });
    $("#nav-analytics").click(function(){
        $(this.parentElement.parentElement.children).removeClass("active");
        self.pageManager.showPage("analytics");
        $(this.parentElement).addClass("active");
    });
    $("#nav-overview").click(function(){
        $(this.parentElement.parentElement.children).removeClass("active");
        self.pageManager.showPage("overview");
        $(this.parentElement).addClass("active");
    });
}
Application.prototype.start = function(timeout){
    this.updater.addMessageHandler(this.pageManager.paint);
    this.updater.start();
}

Application.prototype.updater = {
    socket:null,
    callbacks:[],
    start: function(){
        var url = "ws://" + location.host + "/Nami/monitorsocket";
        if ("WebSocket" in window) {
            this.socket = new WebSocket(url);
        }else{
            this.socket = new MozWebSocket(url);
        }
        var self = this;
        this.socket.onmessage = function(event){
            self.messageArrived(JSON.parse(event.data));
        }
    },
    
    messageArrived: function(data){
        for ( var i = 0; i < callbacks.length; i++){
            this.callbacks[i].func.call(this.callbacks[i].context, data);
        }
    },
    
    addMessageHandler: function(callback, context){
        if (!context)
            context = this
        this.callbacks.push({func:callback, context:context});
    }
    
}

window.onload = function(){
    var app = new Application();
    app.start(5000);
}
