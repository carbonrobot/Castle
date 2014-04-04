using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Castle.Domain
{
    public class UriSafeEntity : Entity
    {
        /// <summary>
        /// Gets or sets the key for use in uri schemas
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new System.ArgumentNullException("value", "Name can not be null or blank");

                _name = value;
                this.Key = _name.ToLowerInvariant().Replace(' ', '-');
            }
        }

        private string _name;
    }
}
