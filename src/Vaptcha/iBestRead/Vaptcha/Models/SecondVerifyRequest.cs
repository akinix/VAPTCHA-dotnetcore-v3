using Newtonsoft.Json;

namespace iBestRead.Vaptcha.Models
{
    public class SecondVerifyRequest
    {
        /// <summary>
        /// 验证单元的VID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 验证单元的Key
        /// </summary>
        [JsonProperty("secretkey")]
        public string SecretKey { get; set; }

        /// <summary>
        /// 验证单元场景，e.g: 0
        /// </summary>
        public int Scene { get; set; }

        /// <summary>
        /// 客户端验证成功得到的token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 获取用户的remote address
        /// </summary>
        public string Ip { get; set; }
        
        public SecondVerifyRequest(string id, string secretKey, int scene, string token, string ip)
        {
            Id = id;
            SecretKey = secretKey;
            Scene = scene;
            Token = token;
            Ip = ip;
        }
    }
}