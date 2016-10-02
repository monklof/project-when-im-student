function extend(Child, Parent){
    var F = function(){};
    F.prototype = Parent.prototype;
    Child.prototype = new F();
    Child.prototype.constructor = Child;
    Child.uper = Parent.prototype;
}

// Base Page
function PageBase(selector, data){
    this.dom = $(selector)[0];
    this.data = data;
}

PageBase.prototype.hide = function(){
    $(this.dom).hide();
    this.handleHide();
}
PageBase.prototype.show = function(){
    $(this.dom).show();
    this.handleShow();//需要由子类来实现
}
PageBase.prototype.handleHide = function(){}
PageBase.prototype.handleShow = function(){}


// Resources Page
function ResourcesPage(selector, data){
    PageBase.call(this, selector, data);
    this.optionsCpuMem = {
        series:{
            lines:{show:true},
            points:{radius:3, show:true}
        },
        xaxis:{
            mode:"time",
            min:1000*60,
            max:1000*60
        },
        yaxis:{
            tickFormatter: function(number){
                return number + "%";
            }
        }
    };
    this.optionsNet = {
        series:{
            lines:{show:true},
            points:{radius:3, show:true}
        },
        xaxis:{
            mode:"time",
            min:1000*60,
            max:1000*60
        },
        yaxis:{
            tickFormatter: function(number){
                return number + "KiB/s";
            }
        }
    };


    this.realtimeData= this.data.realtime;
    this.onehourData = this.data.onehour;
    this.onedayData = this.data.oneday;
    this.oneweekData = this.data.oneweek;
    this.onemonthData =this.data.onemonth;

    this.currentTab = null;
    this._switch("realtime", true);

    var self = this;
    this.updater = {
        socket: null,
        sleep: 500,
        needRunning: true,
        startWrapper: function(context) {
            if (context)
                var self = context;
            else
                var self = this;
            return function(){
                self.needRunning = true;
                var url = "ws://"+location.host + "/Nami/realtimesocket";
                if ("WebSocket" in window){
                    self.socket = new WebSocket(url);
                }else{
                    self.socket = new MozWebSocket(url);
                }
                self.socket.onopen = function(e){
                    console.log ("connect to realtimesocket success!");
                    self.sleep = 500;
                }
                self.socket.onmessage = function(e){
                    self.updateRealtime(JSON.parse(e.data));
                };

                
                self.socket.onclose = function(e){
                    if (!self.needRunning)
                        return;
                    console.log ("connect to realtimesocket closed, will try in ", self.sleep, "ms");
                    setTimeout(self.startWrapper(self), self.sleep);
                    self.sleep *= 2;
                }
                
            }
        },
        updateRealtime: function(data){
            console.log("from realtime server: ", data)
            //cpu
            for (var i = 0 ; i < self.realtimeData.cpu.length; i++){
                self.realtimeData.cpu[i].history.push(data.cpu_rates[i]);
            }
            //mem
            self.realtimeData.mem[0].history.push(data.mem_info.usage)


            if (self.currentTab == "realtime"){
                self._switch("realtime", true);
            }
        },
        close: function(){
            this.needRunning = false;
            this.socket.close();
        }
    };

    this.updater.startWrapper(this.updater)();


    $(this.dom).find(".resources-time").click(function(){
        self._switch($(this).data("id"), false);
    });
}
extend(ResourcesPage, PageBase);

ResourcesPage.prototype.handleHide = function(){
    this.updater.needRunning = false;
    this.updater.close();
}

ResourcesPage.prototype.handleShow = function(){
    this.updater.needRunning = true;
    this.updater.startWrapper()();
}


ResourcesPage.prototype._getSeriesData = function(data, type, deltatime, options){
    // 设置options
    if (!deltatime){
        delete options.xaxis.min;
        delete options.xaxis.max;
    }
    else{
        if (data.length == 0){
            return []
        }
        var history_len = data[0].history.length;
        options.xaxis.max = data[0].history[history_len - 1][0];
        options.xaxis.min = options.xaxis.max - deltatime;
    }

    // 获取曲线数据
    var series = [];
    for (var i = 0; i < data.length; i++){
        switch(type){
        case "cpu":
            series.push({
                label:data[i].processor == -1? "total": "Cpu "+ data[i].processor,
                data:data[i].history
            });
            break;
        case "mem":
            series.push({
                label:data[i].name,
                data:data[i].history
            });
            break;
        case "net":
            series.push({
                label:data[i].name,
                data:data[i].history
            });
            break;
        }
    }
    
    return series;
}


ResourcesPage.prototype._switch = function(tab, force){
    
    if (!force && this.currentTab == tab){
        console.log (tab, "is already showned");
        return;
    }
    var data = null;
    var deltatime = 60*1000;
    switch(tab){
    case "realtime":
        data = this.realtimeData;
        deltatime = 60*1000;
        break;
    case "1hour":
        data = this.onehourData;
        deltatime = 60*60*1000;
        break;
    case "1day":
        data = this.onedayData;
        deltatime = 24*60*60*1000;
        break;
    case "1week":
        data = this.oneweekData;
        deltatime = 7*24*60*60*1000;
        break;
    case "1month":
        data = this.onemonthData;
        deltatime = 30*7*24*60*60*1000;
        break;
    default:
        console.log ("unknown tab", tab);
        return;
    }

    this.currentTab = tab;

    // redraw
    if (this.cpuFlot)
        delete this.cpuFlot;
    if (this.memFlot)
        delete this.memFlot;
    if (this.netFlot)
        delete this.netFlot;
    this.cpuFlot = $.plot($("#cpu-flot-placeholder"), 
                          this._getSeriesData(data.cpu, "cpu", deltatime, this.optionsCpuMem), 
                          this.optionsCpuMem);
    this.memFlot = $.plot($("#mem-flot-placeholder"), 
                          this._getSeriesData(data.mem, "mem", deltatime, this.optionsCpuMem), 
                          this.optionsCpuMem);
    this.netFlot = $.plot($("#net-flot-placeholder"), 
                          this._getSeriesData(data.net, "net", deltatime, this.optionsNet), 
                          this.optionsNet);

    console.log ("changed to tab:", tab);
}

//Processes Page
function ProcessesPage(selector, data){
    PageBase.call(this, selector, data);
    
    var self = this;
    this.selectedPid = null;
    this.sort = {
        select:"cpuRate",
        dir:"down"
    }
    this._refreshTable();

    $(".processinfo-sort").click(function(){
        var col_name = $(this).data("id")
        if (self.sort.select == col_name){
            if (self.sort.dir == "up")
                self.sort.dir = "down";
            else
                self.sort.dir = "up";
        }else{
            self.sort.select = col_name;
            self.sort.dir = "up";
        }
        self._sortTable(col_name, self.sort.dir, self.selectedPid);
        $(".processinfo-sort").removeClass("selected");
        $(this).addClass("selected");
    });


    this.updater = {
        socket: null,
        sleep: 500,
        needRunning: true,
        startWrapper: function(context) {
            if (context)
                var self = context;
            else
                var self = this;
            return function(){
                self.needRunning = true;
                var url = "ws://"+location.host + "/Nami/processessocket";
                if ("WebSocket" in window){
                    self.socket = new WebSocket(url);
                }else{
                    self.socket = new MozWebSocket(url);
                }
                self.socket.onopen = function(e){
                    console.log ("processes://connect to processessocket success!");
                    self.sleep = 500;
                }
                self.socket.onmessage = function(e){
                    self.updateProcesses(JSON.parse(e.data));
                };

                
                self.socket.onclose = function(e){
                    if (!self.needRunning)
                        return;
                    console.log ("processes connect to processessocket closed, will try in ", self.sleep, "ms");
                    setTimeout(self.startWrapper(self), self.sleep);
                    self.sleep *= 2;
                }
                
            }
        },
        updateProcesses: function(data){
            console.log("processes://receive data from realtime server");
            self.data = data;
            self._refreshTable();
        },
        close: function(){
            this.needRunning = false;
            this.socket.close();
        }
    };
    
    //this.updater.startWrapper()();
}
extend(ProcessesPage, PageBase);


ProcessesPage.prototype._refreshTable = function(){
    $("#processes-total").text(this.data.total);
    $("#processes-running").text(this.data.running);
    $("#processes-sleeping").text(this.data.sleeping);
    
    this._sortTable(this.sort.select, this.sort.dir, this.selectedPid);
}

ProcessesPage.prototype._sortTable = function(head, dir, selectedPid){
    heads = ["name", "state", "cpuRate", "memRate", "memResident", "memSize", "pid", "runtime"]
    if (heads.indexOf(head) == -1 || (dir != "down" && dir != "up"))
        return;
    var processInfoList = this.data.processInfo;
    
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
        dom = $("<tr id='{{pid}}' class='process-info'>\
<td>{{name}}</td>\
<td>{{state}}</td>\
<td>{{cpuRate}}</td>\
<td>{{memRate}}</td>\
<td>{{memResident}}</td>\
<td>{{memSize}}</td>\
<td>{{pid}}</td>\
<td>{{runtime}}</td>\
</tr>".replace(/{{pid}}/g, process["pid"]).replace(/{{name}}/g, process["name"]).replace(/{{state}}/g, process["state"]).replace(/{{cpuRate}}/g, process["cpuRate"]).replace(/{{memRate}}/g, process["memRate"]).replace(/{{memResident}}/g, this._size_readable(process["memResident"], "KB")).replace(/{{memSize}}/g, this._size_readable(process["memSize"], "KB")).replace(/{{runtime}}/g, process["runtime"])
         );
        var self = this;
        dom.click(function(){
            if (self.selectedPid == Number(this.id)){
                return;
            }
            
            self.selectedPid = Number(this.id);
            $(".process-info").removeClass("selected");
            $(this).addClass("selected")
        });
        dom.appendTo("#processes-table-tbody");
    }

    adjustTable();
    
    $("#"+selectedPid).addClass("selected");
}

ProcessesPage.prototype._size_readable = function(size, unit){
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

//System Info Page
function SystemInfoPage(selector, data){
    PageBase.call(this, selector, data);
}
extend(SystemInfoPage, PageBase);


window.onload = function(){
    var allData = window.dataObj;

    var resourcesPage = new ResourcesPage("#page-resources-usage", allData.resources);
    var processesPage = new ProcessesPage("#page-running-processes", allData.processes);
    var systemInfoPage = new SystemInfoPage("#page-system-info", allData.systemInfo);
    var currentPage = "resources-usage";
    $(".pagenav").click(function(){
        var page = $(this).data("id");
        if (currentPage == page){
            console.log ("page ", page, " is already shown");
            return
        }
        
        switch(page){
        case "resources-usage":
            processesPage.hide();
            systemInfoPage.hide();
            resourcesPage.show();
            break;
        case "running-processes":
            systemInfoPage.hide();
            resourcesPage.hide();
            processesPage.show();
            break;            
        case "system-info":
            resourcesPage.hide();
            processesPage.hide();
            systemInfoPage.show();
            break;
        default:
            console.log (page, "not exits");
            return;
        }
        
        $(".pagenav").parent().removeClass("active");
        $(this).parent().addClass("active");
        currentPage = page;
        console.log ("switched to page ", page);
    });    
}


// some utils
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

function adjustTable(){
    $("table").children("thead").find("td,th").each(function(){
	    var idx = $(this).index();
		var td = $(this).closest("table").children("tbody")
		                .children("tr:first").children("td,th").eq(idx);
		$(this).width() > td.width() ? td.width($(this).width()) : $(this).width(td.width());
	});
}
