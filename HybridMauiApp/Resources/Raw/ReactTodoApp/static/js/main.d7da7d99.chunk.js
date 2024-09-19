(this["webpackJsonpmoz-todo-react"]=this["webpackJsonpmoz-todo-react"]||[]).push([[0],{14:function(e,t,n){},15:function(e,t,n){"use strict";n.r(t);var a=n(0),c=n.n(a),i=n(7),r=n.n(i),o=(n(14),n(3)),l=n(5),s=n(1);var d=function(e){var t=Object(a.useState)(""),n=Object(s.a)(t,2),i=n[0],r=n[1];return c.a.createElement("form",{onSubmit:function(t){t.preventDefault(),i.trim()&&(e.addTask(i),r(""))}},c.a.createElement("h2",{className:"label-wrapper"},c.a.createElement("label",{htmlFor:"new-todo-input",className:"label__lg"},"What needs to be done?")),c.a.createElement("input",{type:"text",id:"new-todo-input",className:"input input__lg",name:"text",autoComplete:"off",value:i,onChange:function(e){r(e.target.value)}}),c.a.createElement("button",{type:"submit",className:"btn btn__primary btn__lg"},"Add"))};var u=function(e){return c.a.createElement("button",{type:"button",className:"btn toggle-btn","aria-pressed":e.isPressed,onClick:function(){return e.setFilter(e.name)}},c.a.createElement("span",{className:"visually-hidden"},"Show "),c.a.createElement("span",null,e.name),c.a.createElement("span",{className:"visually-hidden"}," tasks"))};function m(e){var t=Object(a.useState)(!1),n=Object(s.a)(t,2),i=n[0],r=n[1],o=Object(a.useState)(""),l=Object(s.a)(o,2),d=l[0],u=l[1],m=Object(a.useRef)(null),b=Object(a.useRef)(null),w=function(e){var t=Object(a.useRef)();return Object(a.useEffect)((function(){t.current=e})),t.current}(i);var f=c.a.createElement("form",{className:"stack-small",onSubmit:function(t){t.preventDefault(),d.trim()&&(e.editTask(e.id,d),u(""),r(!1))}},c.a.createElement("div",{className:"form-group"},c.a.createElement("label",{className:"todo-label",htmlFor:e.id},"New name for ",e.name),c.a.createElement("input",{id:e.id,className:"todo-text",type:"text",value:d||e.name,onChange:function(e){u(e.target.value)},ref:m})),c.a.createElement("div",{className:"btn-group"},c.a.createElement("button",{type:"button",className:"btn todo-cancel",onClick:function(){return r(!1)}},"Cancel",c.a.createElement("span",{className:"visually-hidden"},"renaming ",e.name)),c.a.createElement("button",{type:"submit",className:"btn btn__primary todo-edit"},"Save",c.a.createElement("span",{className:"visually-hidden"},"new name for ",e.name)))),p=c.a.createElement("div",{className:"stack-small"},c.a.createElement("div",{className:"c-cb"},c.a.createElement("input",{id:e.id,type:"checkbox",defaultChecked:e.completed,onChange:function(){return e.toggleTaskCompleted(e.id)}}),c.a.createElement("label",{className:"todo-label",htmlFor:e.id},e.name)),c.a.createElement("div",{className:"btn-group"},c.a.createElement("button",{type:"button",className:"btn",onClick:function(){return r(!0)},ref:b},"Edit ",c.a.createElement("span",{className:"visually-hidden"},e.name)),c.a.createElement("button",{type:"button",className:"btn btn__danger",onClick:function(){return e.deleteTask(e.id)}},"Delete ",c.a.createElement("span",{className:"visually-hidden"},e.name))));return Object(a.useEffect)((function(){!w&&i&&m.current.focus(),w&&!i&&b.current.focus()}),[w,i]),c.a.createElement("li",{className:"todo"},i?f:p)}var b=n(8);window.HybridWebView={Init:function(){function e(e){var t=new CustomEvent("HybridWebViewMessageReceived",{detail:{message:e}});window.dispatchEvent(t)}window.chrome&&window.chrome.webview?window.chrome.webview.addEventListener("message",(function(t){e(t.data)})):window.webkit&&window.webkit.messageHandlers&&window.webkit.messageHandlers.webwindowinterop?window.external={receiveMessage:function(t){e(t)}}:window.addEventListener("message",(function(t){e(t.data)}))},SendRawMessage:function(e){window.HybridWebView.__SendMessageInternal("RawMessage",e)},__SendMessageInternal:function(e,t){var n=e+"|"+t;window.chrome&&window.chrome.webview?window.chrome.webview.postMessage(n):window.webkit&&window.webkit.messageHandlers&&window.webkit.messageHandlers.webwindowinterop?window.webkit.messageHandlers.webwindowinterop.postMessage(n):window.hybridWebViewHost.sendMessage(n)},InvokeMethod:function(e,t,n){if("AsyncFunction"===t[Symbol.toStringTag]){t.apply(void 0,Object(o.a)(n)).then((function(t){window.HybridWebView.__TriggerAsyncCallback(e,t)})).catch((function(e){return console.error(e)}))}else{var a=t.apply(void 0,Object(o.a)(n));window.HybridWebView.__TriggerAsyncCallback(e,a)}},__TriggerAsyncCallback:function(e,t){t&&"string"!==typeof t&&(t=JSON.stringify(t)),window.HybridWebView.__SendMessageInternal("InvokeMethodCompleted",e+"|"+t)}},window.HybridWebView.Init();var w={All:function(){return!0},Active:function(e){return!e.completed},Completed:function(e){return e.completed}},f=Object.keys(w);var p=function(e){var t=Object(a.useState)([]),n=Object(s.a)(t,2),i=n[0],r=n[1],p=Object(a.useState)("All"),g=Object(s.a)(p,2),v=g[0],E=g[1],k=Object(a.useState)(!0),h=Object(s.a)(k,2),y=h[0],N=h[1],O=window.HybridWebView;function j(e,t){var n=JSON.stringify(t);O.SendRawMessage("Invoke:"+e+":"+n)}function _(e){var t=i.map((function(t){return e===t.id?Object(l.a)({},t,{completed:!t.completed}):t}));j("ToggleCompletedTask",[e]),r(t)}function S(e){var t=i.filter((function(t){return e!==t.id}));j("DeleteTask",[e]),r(t)}function C(e,t){var n=i.map((function(n){return e===n.id?Object(l.a)({},n,{name:t}):n}));j("EditTask",[e,t]),r(n)}var T=i.filter(w[v]).map((function(e){return c.a.createElement(m,{id:e.id,name:e.name,completed:e.completed,key:e.id,toggleTaskCompleted:_,deleteTask:S,editTask:C})})),H=f.map((function(e){return c.a.createElement(u,{key:e,name:e,isPressed:e===v,setFilter:E})})),M=1!==T.length?"tasks":"task",x="".concat(T.length," ").concat(M," remaining"),I=Object(a.useRef)(null),W=function(e){var t=Object(a.useRef)();return Object(a.useEffect)((function(){t.current=e})),t.current}(i.length);return window.globalSetData=function(e){return N(!1),r(e),console.log("New data arrived with "+e.length+" item(s)"),"null"},Object(a.useEffect)((function(){i.length-W===-1&&I.current.focus(),console.log("Start loading..."),j("StartTaskLoading")}),[i.length,W]),y?c.a.createElement("div",{className:"todoapp stack-large"},c.a.createElement("h2",null,"Loading items...")):c.a.createElement("div",{className:"todoapp stack-large"},c.a.createElement(d,{addTask:function(e){var t={id:"todo-"+Object(b.a)(),name:e,completed:!1};j("AddTask",[t]),r([].concat(Object(o.a)(i),[t]))}}),c.a.createElement("div",{className:"filters btn-group stack-exception"},H),c.a.createElement("h2",{id:"list-heading",tabIndex:"-1",ref:I},x),c.a.createElement("ul",{className:"todo-list stack-large stack-exception","aria-labelledby":"list-heading"},T))};r.a.render(c.a.createElement(c.a.StrictMode,null,c.a.createElement(p,null)),document.getElementById("root"))},9:function(e,t,n){e.exports=n(15)}},[[9,1,2]]]);
//# sourceMappingURL=main.d7da7d99.chunk.js.map