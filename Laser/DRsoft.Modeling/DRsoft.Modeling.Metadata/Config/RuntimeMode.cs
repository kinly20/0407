namespace DRsoft.Modeling.Metadata.Config
{
    /// <summary>
    /// 运行模式
    /// </summary>
    internal class RuntimeMode
    {
        /// <summary>
        /// 验证签名的公钥
        /// </summary>
        private static readonly string s_pubkey = "<RSAKeyValue><Modulus>m+KTlepNNbDJr49qvD/19g0uMRl9TgzhWfnxrsQqgvMg4XdlVcERpm3pwvTfpxTsncgt0/jD7kR2c283r50MOw3dHk3MHQ56ON6xk98RIwZUIcHbPSaydzQKB+AYaY4HxHsu2MUSGAesGR548Y77XgpnIROmqnHQunNJBwf1h4s=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// 是否平台开发
        /// </summary>
        private static bool s_isPlatformDeveloper = false;

        private static RuntimeMode s_mode;
        /// <summary>
        /// 获取当前运行模式
        /// </summary>
        public static RuntimeMode Current
        {
            get
            {
                return s_mode = s_mode ?? new RuntimeMode();
            }
            internal set => s_mode = value;
        }

        static RuntimeMode()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsPlatformDeveloper => s_isPlatformDeveloper;

        /// <summary>
        /// 是否预览模式
        /// </summary>
        public static bool IsPreView => false;

        //get { return s_IsPreview; }
        /// <summary>
        /// 是否设计时
        /// </summary>
        public static bool IsDesign => false;
    }
}
