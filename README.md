# 小花仙 一键截图工具 支持4K、8K高清截图
小花仙 一键截图工具 支持4K、8K高清截图

## 兼容性
环境：16GB内存 i5 1035G1 CPU MX350 N卡 使用7680x4320 放大5倍 视野移动 (-4000,-2000)

<!--
需手动设置快捷方式并从快捷方式手动启动的方法：
1. 从别的浏览器的```User Data```目录找到```PepperFlash```目录拷出来。或者网上找一个PPAPI Flash Player绿色版。里面通常带有```pepflashplayer.dll```文件。
2. 右键这个DLL文件属性，详细信息，看一下版本，版本29或以下的不用管这一步了。如果版本大于或等于30，需要从hosts文件屏蔽一些域名以解决“此Flash Player与您的地区不相容”。具体自行百度。
3. 找到浏览器启动的EXE文件，按住Alt键鼠标随便往空白处拖拉，得到一个快捷方式。右键这个快捷方式，在“目标”处里面最后空一格添加这些参数：
```bash
--ppapi-flash-path=你事先准备好的pepflashplayer.dll文件的绝对路径（如果有路径空格需要加双引号） --ppapi-flash-version=99.0.0.999 --remote-debugging-port=9222
```
-->

| 浏览器      | Puppeteer模式 | 备注     | Selenium模式 | 备注     |
| :---:        |    ----   |          ---  | --- | --- |
| 双核浏览器<img width=400/>      | ×       | Flash崩溃   | × | Flash崩溃<img width=1800/> |
| 百分浏览器   | √        |   | √ |  |
| QQ浏览器 | × | 无法接入浏览器 | × | Flash崩溃
| 星愿浏览器 | √ |  |  √ |  |
| Edge浏览器（内核87） | √ | Edge默认关闭Flash，因此需要需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面（1月21日后更新88版本，此版本开始将无法使用Flash） | × | 无法接入浏览器（Edge谷歌内核版也需要专门的Edge驱动）
| Chrome浏览器（[内核87便携版](http://www.epinv.com/post/7888.html)）| √ | Chrome默认关闭Flash，因此需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面 | √ | Chrome默认关闭Flash，因此需要需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面 |
| Chromium浏览器（内核79） | × | 指定Flash组件DLL路径后显示Flash因过期被停用，点击运行一次后无反应（考虑添加```--allow-outdated-plugins```） | × | CDP报错：Unable to capture screenshot. (无法截图) |
| 360安全浏览器 | | 还未测试 | | 还未测试 |
| 360极速浏览器 | | 还未测试 | | 还未测试 |
| 搜狗浏览器 | × | 无法接入浏览器 | × | 无法接入浏览器 |
| 猎豹浏览器 | × | 启动基座失败 | × | Flash崩溃 |

## 截屏效果
![](https://img-blog.csdnimg.cn/20201221012555410.jpg)
