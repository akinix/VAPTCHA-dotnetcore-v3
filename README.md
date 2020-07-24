# VAPTCHA SDK for dotnetcore

[![Build](https://github.com/iBestRead/VAPTCHA-dotnetcore-v3/workflows/Build/badge.svg)](https://github.com/iBestRead/VAPTCHA-dotnetcore-v3/actions?query=workflow%3A%22Build%22)
[![NuGetVersion](https://img.shields.io/nuget/v/vaptcha.sdk.dotnetcore)](https://www.nuget.org/packages/vaptcha.sdk.dotnetcore)

方便asp.net core 项目集成 `VAPTCHA` 验证码.

# 安装Nuget包

```shell
dotnet add package vaptcha.sdk.dotnetcore
```

# 配置

- 在`Startup.cs`的`ConfigureServices`中注入服务:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVaptcha(Configuration);
    // ...
}
```

- 修改`appsettings.json`文件:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Vaptcha": {
    "SecretKey": "登录VAPTCHA后台,复制KEY",
    "Vid": "登录VAPTCHA后台,复制VID",
    "Scene": 0
  }
}
```

# 示例

在`samples`文件夹下存放了一个示例项目,可以调试运行,需要修改`Login.cshtml`末尾的代码

```js
<script>
    vaptcha({
        //配置参数
        vid: '@Options.Value.Vid', // 替换成自己的验证单元id
        type: 'click', // 展现类型 点击式
        container: '#vaptchaContainer'
        // 按钮容器，可为Element 或者 selector
        // ... //其他配置参数省略
    }).then(function (vaptchaObj) {
        vaptchaObj.listen("pass", function () {
            $("#login-submit").removeAttr("disabled");
        });
        vaptchaObj.render();
        vaptchaObj.renderTokenInput('account');  //以form的方式提交数据时，使用此函数向表单添加token值
    })
</script>
```

