layui.define(['table', 'layer'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer,
        table = layui.table,
        ankou = {
            //批量操作
            batOperate: function (url, key) {
                var checkStatus = table.checkStatus('list');
                if (checkStatus.data.length > 0) {
                    var data = checkStatus.data;
                    layer.confirm('确认操作吗?', function (index) {
                        //请求
                        $.ajax({
                            url: url,
                            type: 'POST',
                            data: { ids: data.map(function (e) { return e[key]; }) },
                            success: function (res) {
                                if (res.success === true) {
                                    //成功的提示
                                    layer.msg(res.msg, { icon: 1, time: 1000 }, function () {
                                        //重新加载数据
                                        var loading = layer.msg('数据请求中', { icon: 16, shade: 0.01, time: 0 });
                                        table.reload('list', {
                                            done: function (res, curr, count) {
                                                layer.close(loading);
                                            }
                                        });
                                    });
                                }
                                else {
                                    layer.msg(res.msg, { icon: 2, time: 1000 });
                                }
                            },
                            error: function (err) {
                                layer.msg(err, { icon: 2, time: 1000 });
                            }
                        });
                    });
                }
                else {
                    layer.msg("请选择要操作的对象!", { icon: 2, time: 1000 });
                    return;
                }
            },
            //单个操作
            operate: function (url, data) {
                layer.confirm('确认操作吗?', function (index) {
                    //请求
                    $.ajax({
                        url: url,
                        type: 'POST',
                        data: data,
                        success: function (res) {
                            if (res.success === true) {
                                //成功的提示
                                layer.msg(res.msg, { icon: 1, time: 1000 }, function () {
                                    //重新加载数据
                                    var loading = layer.msg('数据请求中', { icon: 16, shade: 0.01, time: 0 });
                                    table.reload('list', {
                                        done: function (res, curr, count) {
                                            layer.close(loading);
                                        }
                                    });
                                });
                            }
                            else {
                                layer.msg(res.msg, { icon: 2, time: 1000 });
                            }
                        },
                        error: function (err) {
                            layer.msg(err, { icon: 2, time: 1000 });
                        }
                    });
                });
            },
            //通用发送请求
            request: function (url, type, data, callBack) {
                //loading层
                var loading2 = layer.load(1, {
                    shade: [0.1, '#fff'] //0.1透明度的白色背景
                });
                //请求
                $.ajax({
                    url: url,
                    type: type,
                    data: data,
                    success: function (res) {
                        layer.close(loading2);
                        if (res.success === true) {
                            if (typeof (callBack) === "function") {
                                callBack(res);
                            }
                        }
                        else {
                            layer.msg(res.msg, { icon: 2, time: 1000 });
                        }
                    },
                    error: function (err) {
                        layer.msg(err, { icon: 2, time: 1000 });
                    }
                });
            },
            //打开窗口
            open: function (title, url, width, height) {
                //如果是移动端，就使用自适应大小弹窗
                if (navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i)) {
                    width = 'auto';
                    height = 'auto';
                }
                layer.open({
                    type: 2, title: title,
                    content: url,
                    area: [width + 'px', height + 'px']
                });
            },
            // 弹出层全屏
            openFull: function (title, url, width, height) {
                //如果是移动端，就使用自适应大小弹窗
                if (navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i)) {
                    width = 'auto';
                    height = 'auto';
                }

                var index = layer.open({
                    type: 2,
                    area: [width + 'px', height + 'px'],
                    fix: false,
                    //不固定
                    maxmin: true,
                    shade: 0.3,
                    title: title,
                    content: url
                });
                layer.full(index);
            },
            // 保存信息
            save: function (url, data, callback) {
                //loading层
                var loading2 = layer.load(1, {
                    shade: [0.1, '#fff'] //0.1透明度的白色背景
                });

                //请求
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: data,
                    success: function (res) {
                        layer.close(loading2);
                        if (res.success === true) {
                            //成功的提示
                            layer.msg(res.msg, { icon: 1, time: 1000 }, function () {
                                var llayer = window.parent.layer;
                                //关闭窗口
                                var index = parent.layer.getFrameIndex(window.name);
                                llayer.close(index);
                                //刷新父窗口表格
                                if (window.parent.layui.table) {
                                    var loading = llayer.msg('数据请求中', { icon: 16, shade: 0.01, time: 0 });
                                    window.parent.layui.table.reload('list', {
                                        done: function (res, curr, count) {
                                            llayer.close(loading);
                                        }
                                    });
                                }
                                //预留回调
                                if (typeof (callback) === "function") {
                                    callback();
                                }
                            });
                        }
                        else {
                            layer.msg(res.msg, { icon: 2, time: 1000 });
                        }
                    },
                    error: function (err) {
                        layer.msg(err, { icon: 2, time: 1000 });
                    }
                });
            },
            // 关闭窗体
            close: function () {
                var index = parent.layer.getFrameIndex(window.name);
                parent.layer.close(index);
            },
            //关闭当前窗口，重载列表
            reload: function () {
                var llayer = window.parent.layer;
                //关闭窗口
                var index = parent.layer.getFrameIndex(window.name);
                llayer.close(index);
                //刷新父窗口表格
                if (window.parent.layui.table) {
                    var loading = llayer.msg('数据请求中', { icon: 16, shade: 0.01, time: 0 });
                    window.parent.layui.table.reload('list', {
                        done: function (res, curr, count) {
                            llayer.close(loading);
                        }
                    });
                }
            },
            //绑定数据
            dataBind: function (url, where, reset) {
                //loading
                var loading = layer.msg('数据请求中', {
                    icon: 16, shade: 0.01, time: 0
                });
                var options;
                //重置第一页
                if (reset) {
                    options = {
                        url: url,
                        where: where,
                        //数据渲染完成的回调
                        done: function (res, curr, count) {
                            layer.close(loading);
                        },
                        page: {
                            curr: 1 //重新从第 1 页开始
                        }
                    };
                } else {
                    options = {
                        url: url,
                        where: where,
                        //数据渲染完成的回调
                        done: function (res, curr, count) {
                            layer.close(loading);
                        }
                    };
                }
                table.reload('list', options);
            }
        };

    //接口输出
    exports('ankou', ankou);
});


//扩展Date实例方法，格式化日期
(function () {
    //基础扩展Start
    //日期扩展
    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1, //月份   
            "d+": this.getDate(), //日   
            "H+": this.getHours(), //小时   
            "m+": this.getMinutes(), //分   
            "s+": this.getSeconds(), //秒   
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
            "S": this.getMilliseconds() //毫秒   
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o) {
            if (o.hasOwnProperty(k)) {
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };

    //字符串扩展
    String.prototype.getDate = function () {
        var date = new Date(this);
        return date.format("yyyy-MM-dd HH:mm:ss");
    };
    //字符串扩展
    String.prototype.getShortDate = function () {
        var date = new Date(this);
        return date.format("yyyy-MM-dd");
    };

    //字符串扩展
    String.prototype.getTime = function () {
        var date = new Date(this);
        return date.format("HH:mm");
    };
})();