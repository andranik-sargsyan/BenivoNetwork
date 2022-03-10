namespace BenivoNetwork.Common.Models
{
    public class WelcomeModel
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public RegisterModel RegisterModel { get; set; } = new RegisterModel();
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }

        public WelcomeModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
            LoginModel.ReturnUrl = returnUrl;
            RegisterModel.ReturnUrl = returnUrl;
        }

        public WelcomeModel(LoginModel model)
        {
            LoginModel = model;
            LoginModel.ParentModel = this;
            LoginModel.ParentModel.ReturnUrl = model.ReturnUrl;
        }

        public WelcomeModel(RegisterModel model)
        {
            RegisterModel = model;
            RegisterModel.ParentModel = this;
            RegisterModel.ParentModel.ReturnUrl = model.ReturnUrl;
        }
    }
}
