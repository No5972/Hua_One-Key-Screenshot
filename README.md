# 小花仙 一键截图工具 支持4K、8K高清截图
小花仙 一键截图工具 支持4K、8K高清截图 [![GitHub release (latest by date)](https://img.shields.io/github/v/release/No5972/Hua_One-Key-Screenshot?label=%E7%82%B9%E6%AD%A4%E4%B8%8B%E8%BD%BD%E6%9C%80%E6%96%B0%E7%89%88%E6%9C%AC%EF%BC%88%E7%AC%AC%E4%B8%80%E4%B8%AA%E6%96%87%E4%BB%B6%EF%BC%89)](https://github.com/No5972/Hua_One-Key-Screenshot/releases/latest)

**注意：经过实测，本工具只支持接入64位的浏览器，32位的浏览器截图过程中Flash崩溃的几率很大，因此需使用64位的浏览器来操作。**

## 操作方法
1. 先打开一键截图工具
2. 如果你的浏览器可以直接使用Flash，那么单选框不用管；如果你的浏览器需要手动指定Flash路径，那么根据实际情况选择指定Flash的方式，如果使用指定路径的Flash还需要浏览选择Flash组件DLL的位置（如果没有的话需要去网上搜索下载Flash Player PPAPI绿色版）。
3. 点击启动浏览器，找到使用的浏览器的启动程序。
4. 登录小花仙账号（本程序在本页面开源，如果实在担心不安全可以选择删除本工具不使用），如果截当前场景的玩家的形象则直接点开这个玩家的面板。如果需要截指定米米号的形象，则进花灵派对，点击逛一逛，输入你要查看的米米号，确定，然后点击右上方的头像，再点社区形象。注意这个米米号的用户的花灵派对权限设置要保证当前登录用户可以访问，
5. 面板打开后点击本窗口的截图按钮，稍等片刻。
6. 截图完成后生成的PNG文件在“我的文档”目录下，以日期时间格式命名。

## 兼容性
环境：16GB内存 i5 1035G1 CPU MX350 N卡 使用7680x4320 放大5倍 视野移动 (-4500,-2000)

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
| 百分浏览器   | √        |  ~~需选择“使用浏览器内置Flash”的单选框~~ 自动下载的Flash　32版本已经在1月12日无法使用，需选择“使用指定路径的Flash”的单选框并手动指定Flash路径 追加：必须使用64位的浏览器和64位的Flash组件文件，不然截图过程中Flash会崩溃。 | √ | ~~需选择“使用浏览器内置Flash”的单选框~~ 自动下载的Flash　32版本已经在1月12日无法使用，需选择“使用指定路径的Flash”的单选框并手动指定Flash路径 追加：必须使用64位的浏览器和64位的Flash组件文件，不然截图过程中Flash会崩溃。 |
| QQ浏览器 | × | 无法接入浏览器 | × | Flash崩溃
| 星愿浏览器 | √ | 需选择“使用指定路径的Flash”的单选框并手动指定Flash路径 |  √ | 需选择“使用指定路径的Flash”的单选框并手动指定Flash路径 |
| Edge浏览器（内核87） | √ | Edge默认关闭Flash，因此需要需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，~~回到游戏页面刷新页面~~ 米叔禁止了版本32的Flash的加载，需要使用某些特殊手段强制版本32的Flash的加载。（1月21日后更新88版本，此版本开始将无法使用Flash） | × | 无法接入浏览器（Edge谷歌内核版也需要专门的Edge驱动）
| Chrome浏览器（[内核87便携版](http://www.epinv.com/post/7888.html)）| √ | Chrome默认关闭Flash，因此需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面（1月19日后更新88版本，此版本开始将无法使用Flash） | √ | Chrome默认关闭Flash，因此需要需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面（1月19日后更新88版本，此版本开始将无法使用Flash） |
| Chromium浏览器（[内核78](http://www.downza.cn/soft/217234.html)） | × | 指定Flash组件DLL路径后显示Flash因过期被停用，点击运行一次后无反应，关闭浏览器后Puppeteer报错"Failed to load Pepper module"（加载Pepper模块失败） | √ | Chromium默认关闭Flash，因此需要需要手动开启Flash。点击“进行游戏需要Flash Player”，再点击地址栏左边的不安全，点站点权限，把Flash改成允许，回到游戏页面刷新页面 |
| 360安全浏览器 | √ | 默认使用的小花仙游戏页面锁版本32的Flash，需要手动访问[小花仙Flash的URL](http://hua.61.com/Client.swf)来操作，这个可以正常截图 | × | Flash崩溃 |
| 360极速浏览器 | √ | 默认使用的小花仙游戏页面锁版本32的Flash，需要手动访问[小花仙Flash的URL](http://hua.61.com/Client.swf)来操作，但有几率出现人物消失的问题，需要多次尝试 | × | Flash崩溃 |
| 搜狗浏览器 | × | 无法接入浏览器 | × | 无法接入浏览器 |
| 猎豹浏览器 | × | 启动基座失败 | × | Flash崩溃 | 
| UC浏览器 | × | 只有版本55的内核，用的还是NPAPI（测试环境已经安装了移除Flash的系统更新） | × | 只有版本55的内核，用的还是NPAPI（测试环境已经安装了移除Flash的系统更新） |

## 工具截图
![](https://s3.ax1x.com/2020/12/21/r0KBIH.png)

## 截屏效果（可右键查看原图）
![](https://img-blog.csdnimg.cn/20201221012555410.jpg)
