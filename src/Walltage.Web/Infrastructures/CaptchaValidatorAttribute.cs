using System.Web.Mvc;

namespace Walltage.Web.Infrastructures
{
    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";
        private const string SECRET_KEY = "6LdQAw0UAAAAAJAmnlqQGQLPzQIUynOmwUpEkQVu";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool valid = false;
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];

            if (!string.IsNullOrEmpty(captchaChallengeValue) && !string.IsNullOrEmpty(captchaResponseValue))
            {
                // validate recaptcha
                var captchaValidator = new Recaptcha.RecaptchaValidator
                {
                    PrivateKey = SECRET_KEY,
                    RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                    Challenge = captchaChallengeValue,
                    Response = captchaResponseValue
                };

                var recaptchaResponse = captchaValidator.Validate();
                valid = recaptchaResponse.IsValid;
            }

            // this will push the result value into a parameter in our Action
            filterContext.ActionParameters["captchaValid"] = valid;

            base.OnActionExecuting(filterContext);
        }
    }
}