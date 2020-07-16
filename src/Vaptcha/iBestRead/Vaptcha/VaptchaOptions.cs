namespace iBestRead.Vaptcha
{
    public class VaptchaOptions
    {
        /// <summary>
        /// 验证单元key
        /// </summary>
        public string SecretKey { get; set; }
        
        /// <summary>
        /// 验证单元id
        /// </summary>
        public string Vid { get; set; }

        /// <summary>
        /// 场景值
        /// </summary>
        public int Scene { get; set; } = 0;
    }
}