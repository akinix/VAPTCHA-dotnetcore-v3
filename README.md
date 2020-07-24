# VAPTCHA SDK for dotnetcore

[![Build](https://github.com/iBestRead/VAPTCHA-dotnetcore-v3/workflows/Build/badge.svg)](https://github.com/iBestRead/VAPTCHA-dotnetcore-v3/actions?query=workflow%3A%22Build%22)
[![NuGetVersion](https://img.shields.io/nuget/v/vaptcha.sdk.dotnetcore)](https://www.nuget.org/packages/vaptcha.sdk.dotnetcore)

����asp.net core ��Ŀ���� `VAPTCHA` ��֤��.

# ��װNuget��

```shell
dotnet add package vaptcha.sdk.dotnetcore
```

# ����

- ��`Startup.cs`��`ConfigureServices`��ע�����:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddVaptcha(Configuration);
    // ...
}
```

- �޸�`appsettings.json`�ļ�:

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
    "SecretKey": "��¼VAPTCHA��̨,����KEY",
    "Vid": "��¼VAPTCHA��̨,����VID",
    "Scene": 0
  }
}
```

# ʾ��

��`samples`�ļ����´����һ��ʾ����Ŀ,���Ե�������,��Ҫ�޸�`Login.cshtml`ĩβ�Ĵ���

```js
<script>
    vaptcha({
        //���ò���
        vid: '@Options.Value.Vid', // �滻���Լ�����֤��Ԫid
        type: 'click', // չ������ ���ʽ
        container: '#vaptchaContainer'
        // ��ť��������ΪElement ���� selector
        // ... //�������ò���ʡ��
    }).then(function (vaptchaObj) {
        vaptchaObj.listen("pass", function () {
            $("#login-submit").removeAttr("disabled");
        });
        vaptchaObj.render();
        vaptchaObj.renderTokenInput('account');  //��form�ķ�ʽ�ύ����ʱ��ʹ�ô˺���������tokenֵ
    })
</script>
```

