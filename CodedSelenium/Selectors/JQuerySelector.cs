using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Selectors
{
    public class JQuerySelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JQuerySelector" /> class.
        /// </summary>
        /// <param name="tagName">Element tag name. Example: 'div'</param>
        /// <param name="contentFilters">jQuery content filters. Example: ':contains()'</param>
        /// <param name="functionFilters">Custom JavaScript filters. Example: '($(this).text() === \"Control inner text\")'</param>
        /// <param name="attributes">jQuery like attributes key value pairs. Example: [id*=customId]</param>
        public JQuerySelector(string tagName, List<string> contentFilters, List<string> functionFilters, List<string> attributes)
        {
            this.TagName = tagName;
            this.ContentFilters = contentFilters;
            this.FunctionFilters = functionFilters;
            this.Attributes = attributes;
        }

        /// <summary>
        /// Gets or sets jQuery element tag name. Example: 'div'
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets jQuery content filters. Example: ':contains()'
        /// </summary>
        public List<string> ContentFilters { get; set; }

        /// <summary>
        /// Gets or sets Custom JavaScript filters. Example: '($(this).text() === \"Control inner text\")'
        /// </summary>
        public List<string> FunctionFilters { get; set; }

        /// <summary>
        /// Gets or sets jQuery like attributes key value pairs. Example: [id*=customId]
        /// </summary>
        public List<string> Attributes { get; set; }

        /// <summary>
        /// jQuery selector with %parent% placeholder
        /// </summary>
        /// <returns>jQuery selector string with %parent% placeholder</returns>
        public override string ToString()
        {
            string filter = string.Empty;
            if (this.FunctionFilters.Count != 0)
            {
                string functionTemplate = ".filter(function() { return %scriptStatements%;})";
                filter = functionTemplate.Replace("%scriptStatements%", string.Join(" && ", this.FunctionFilters));
            }

            string selector = string.Format(
                "jQuery(\"{0}{1}{2}\"%parent%){3}",
                this.TagName,
                string.Join(string.Empty, this.Attributes),
                string.Join(string.Empty, this.ContentFilters),
                filter);

            return selector;
        }
    }
}
