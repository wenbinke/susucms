using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Web.WebPages.Scope;

namespace SusuCMS.Web.Front.Validation
{
    public sealed class ValidationHelper
    {
        // Fields
        private readonly HttpContextBase _httpContext;
        private static readonly object _invalidCssClassKey = new object();
        private readonly ModelStateDictionary _modelStateDictionary;
        private static IDictionary<object, object> _scopeOverride;
        private readonly Dictionary<string, List<IValidator>> _validators = new Dictionary<string, List<IValidator>>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _validCssClassKey = new object();

        // Methods
        internal ValidationHelper(HttpContextBase httpContext, ModelStateDictionary modelStateDictionary)
        {
            this._httpContext = httpContext;
            this._modelStateDictionary = modelStateDictionary;
        }

        public void Add(IEnumerable<string> fields, params IValidator[] validators)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }
            if (validators == null)
            {
                throw new ArgumentNullException("validators");
            }
            foreach (string str in fields)
            {
                this.Add(str, validators);
            }
        }

        public void Add(string field, params IValidator[] validators)
        {
            if ((validators == null) || validators.Any<IValidator>(v => (v == null)))
            {
                throw new ArgumentNullException("validators");
            }
            this.AddFieldValidators(field, validators);
        }

        private void AddFieldValidators(string field, params IValidator[] validators)
        {
            List<IValidator> list = null;
            if (!this._validators.TryGetValue(field, out list))
            {
                list = new List<IValidator>();
                this._validators[field] = list;
            }
            foreach (IValidator validator in validators)
            {
                list.Add(validator);
            }
        }

        public HtmlString ClassFor(string field)
        {
            if ((this._httpContext != null) && string.Equals("POST", this._httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                string str = this.IsValid(new string[] { field }) ? ValidCssClass : InvalidCssClass;
                if (str != null)
                {
                    return new HtmlString(str);
                }
            }
            return null;
        }

        public HtmlString For(string field)
        {
            return GenerateHtmlFromClientValidationRules(this.GetClientValidationRules(field));
        }

        internal static HtmlString GenerateHtmlFromClientValidationRules(IEnumerable<ModelClientValidationRule> clientRules)
        {
            if (!clientRules.Any<ModelClientValidationRule>())
            {
                return null;
            }
            Dictionary<string, object> results = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules, results);
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, object> pair in results)
            {
                string key = pair.Key;
                string str2 = Convert.ToString(pair.Value, CultureInfo.InvariantCulture);
                builder.Append(key).Append("=\"").Append(str2).Append('"').Append(' ');
            }
            if (builder.Length > 0)
            {
                builder.Length--;
            }
            return new HtmlString(builder.ToString());
        }

        private IEnumerable<ModelClientValidationRule> GetClientValidationRules(string field)
        {
            List<IValidator> list = null;
            if (!this._validators.TryGetValue(field, out list))
            {
                return Enumerable.Empty<ModelClientValidationRule>();
            }
            return (from item in list
                    let clientRule = item.ClientValidationRule
                    where clientRule != null
                    select clientRule);
        }

        public IEnumerable<string> GetErrors(params string[] fields)
        {
            return (from r in this.Validate(fields) select r.ErrorMessage);
        }

        internal IDictionary<string, object> GetUnobtrusiveValidationAttributes(string field)
        {
            IEnumerable<ModelClientValidationRule> clientValidationRules = this.GetClientValidationRules(field);
            Dictionary<string, object> results = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientValidationRules, results);
            return results;
        }

        public bool IsValid(params string[] fields)
        {
            return !this.Validate(fields).Any<ValidationResult>();
        }

        internal static IDisposable OverrideScope()
        {
            _scopeOverride = new Dictionary<object, object>();
            return new DisposableAction(delegate
            {
                _scopeOverride = null;
            });
        }

        public void RequireField(string field)
        {
            string errorMessage = null;
            this.RequireField(field, errorMessage);
        }

        public void RequireField(string field, string errorMessage)
        {
            IValidator[] validators = new IValidator[1];
            string str = errorMessage;
            validators[0] = System.Web.WebPages.Validator.Required(str);
            this.Add(field, validators);
        }

        public void RequireFields(params string[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }
            foreach (string str in fields)
            {
                this.RequireField(str);
            }
        }

        public IEnumerable<ValidationResult> Validate(params string[] fields)
        {
            IEnumerable<string> keys = fields;
            if ((fields == null) || !fields.Any<string>())
            {
                keys = this._validators.Keys;
            }
            return this.ValidateFieldsAndUpdateModelState(keys);
        }

        private IEnumerable<ValidationResult> ValidateField(string field, ValidationContext context)
        {
            List<IValidator> list;
            if (!this._validators.TryGetValue(field, out list))
            {
                return Enumerable.Empty<ValidationResult>();
            }
            context.MemberName = field;
            return (from f in list
                    select f.Validate(context) into result
                    where result != ValidationResult.Success
                    select result);
        }

        private IEnumerable<ValidationResult> ValidateFieldsAndUpdateModelState(IEnumerable<string> fields)
        {
            IServiceProvider serviceProvider = null;
            IDictionary<object, object> items = null;
            ValidationContext context = new ValidationContext(this._httpContext, serviceProvider, items);
            List<ValidationResult> list = new List<ValidationResult>();
            foreach (string str in fields)
            {
                IEnumerable<ValidationResult> collection = this.ValidateField(str, context);
                IEnumerable<string> first = from c in collection select c.ErrorMessage;
                ModelState state = this._modelStateDictionary[str];
                if (state != null)
                {
                    first = first.Except<string>(state.Errors.Select(i => i.ErrorMessage), StringComparer.OrdinalIgnoreCase);
                }
                foreach (string str2 in first)
                {
                    this._modelStateDictionary.AddModelError(str, str2);
                }
                list.AddRange(collection);
            }
            return list;
        }

        // Properties
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This makes it easier for a user to read this value without knowing of this type.")]
        public string FormField
        {
            get
            {
                return "_FORM";
            }
        }

        public static string InvalidCssClass
        {
            get
            {
                object obj2;
                if (!Scope.TryGetValue(_invalidCssClassKey, out obj2))
                {
                    return "input-validation-error";
                }
                return (obj2 as string);
            }
            set
            {
                Scope[_invalidCssClassKey] = value;
            }
        }

        internal static IDictionary<object, object> Scope
        {
            get
            {
                return (_scopeOverride ?? ScopeStorage.CurrentScope);
            }
        }

        public static string ValidCssClass
        {
            get
            {
                object obj2;
                if (!Scope.TryGetValue(_validCssClassKey, out obj2))
                {
                    return null;
                }
                return (obj2 as string);
            }
            set
            {
                Scope[_validCssClassKey] = value;
            }
        }
    }

    internal class DisposableAction : IDisposable
    {
        // Fields
        private Action _action;
        private bool _hasDisposed;

        // Methods
        public DisposableAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this._action = action;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (!this._hasDisposed)
                    {
                        this._hasDisposed = true;
                        this._action();
                    }
                }
            }
        }
    }
}
