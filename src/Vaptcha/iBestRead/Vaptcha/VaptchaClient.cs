using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using iBestRead.Vaptcha.Consts;
using iBestRead.Vaptcha.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace iBestRead.Vaptcha
{
    public class VaptchaClient : IVaptchaClient
    {
        private readonly ILogger _logger;
        private readonly VaptchaOptions _vaptchaOptions;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public VaptchaClient(
            ILoggerFactory loggerFactory,
            IOptions<VaptchaOptions> options,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger<VaptchaClient>();
            _vaptchaOptions = options.Value;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SecondVerifyResponse> SecondVerifyAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new SecondVerifyResponse((int)VerifyResult.Fail, "验证失败,Verify_Token为空.");
            
            if(token.Length < 7)
                return new SecondVerifyResponse((int)VerifyResult.Fail, "验证失败,Verify_Token格式异常.");
            
            // 根据token前7位判断验证模式
            var mode = token.Substring(0, 7);

            // 离线模式
            if (VaptchaConsts.OfflineMode.Equals(mode, StringComparison.OrdinalIgnoreCase))
            {
                return new SecondVerifyResponse((int)VerifyResult.Fail, "验证失败,暂不支持离线模式.");
            }

            return await VerifyFromVaptchaAsync(token, GetRemoteIpAddress());
        }

        private async Task<SecondVerifyResponse> VerifyFromVaptchaAsync(string token, string ip)
        {
            try
            {
                var uri = new Uri(VaptchaConsts.SecondVerifyUrl);
                var request = new SecondVerifyRequest(_vaptchaOptions.Vid, _vaptchaOptions.SecretKey, _vaptchaOptions.Scene, token, ip);
                var httpContent = new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8,
                    "application/json");

                var httpClient =  _httpClientFactory.CreateClient(VaptchaConsts.HttpClientName);
                var response = await httpClient.PostAsync(uri, httpContent);
            
                response.EnsureSuccessStatusCode();
                
                var strResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SecondVerifyResponse>(strResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new SecondVerifyResponse((int)VerifyResult.Fail, "验证失败,请求Vaptcha Api 发生异常.");
            }
        }

        private string GetRemoteIpAddress()
        {
            return _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress.ToString();
        }
        
    }
}