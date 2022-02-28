namespace BenivoNetwork.Common.Models
{
    public class WelcomeModel
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public RegisterModel RegisterModel { get; set; } = new RegisterModel();
        public string ReturnUrl { get; set; }

        public WelcomeModel()
        {
        }

        public WelcomeModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
            LoginModel.ReturnUrl = returnUrl;
            RegisterModel.ReturnUrl = returnUrl;
        }

        public WelcomeModel(LoginModel model) : this(model.ReturnUrl)
        {
            LoginModel = model;
        }

        public WelcomeModel(RegisterModel model) : this(model.ReturnUrl)
        {
            RegisterModel = model;
        }
    }
}
