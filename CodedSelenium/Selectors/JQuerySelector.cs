using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedSelenium.Selectors
{
    public class JQuerySelector
    {
        private PropertyExpressionCollection searchProperties;
        private PropertyExpressionCollection filterProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="JQuerySelector" /> class.
        /// </summary>
        /// <param name="tagName">Element tag name. Example: 'div'</param>
        /// <param name="contentFilters">jQuery content filters. Example: ':contains()'</param>
        /// <param name="functionFilters">Custom JavaScript filters. Example: '($(this).text() === \"Control inner text\")'</param>
        /// <param name="attributes">jQuery like attributes key value pairs. Example: [id*=customId]</param>
        public JQuerySelector(string tagName, List<string> contentFilters, List<string> functionFilters, List<string> attributes)
        {
            TagName = tagName;
            ContentFilters = contentFilters;
            FunctionFilters = functionFilters;
            Attributes = attributes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JQuerySelector" /> class.
        /// </summary>
        /// <param name="searchProperties">Collection of search properties</param>
        /// <param name="filterProperties">Collection of filter properties</param>
        public JQuerySelector(PropertyExpressionCollection searchProperties, PropertyExpressionCollection filterProperties)
        {
            this.searchProperties = searchProperties;
            this.filterProperties = filterProperties;
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
        /// jQuery selector. Example 'jQuery("div[id*=customId]")'
        /// </summary>
        /// <param name="parent">Parent <see cref="JQuerySelector" />. Pass null if there is no one.</param>
        /// <returns>jQuery selector string</returns>
        public string ToString(JQuerySelector parent)
        {
            string filter = string.Empty;
            if (FunctionFilters.Count != 0)
            {
                string functionTemplate = ".filter(function() { return %scriptStatements%;})";
                filter = functionTemplate.Replace("%scriptStatements%", string.Join(" && ", FunctionFilters));
            }

            string selector = string.Format(
                "jQuery(\"{0}{1}{2}\"%parent%){3}",
                TagName,
                string.Join(string.Empty, Attributes),
                string.Join(string.Empty, ContentFilters),
                filter);

            return selector.Replace("%parent%", parent != null ? ", " + parent.ToString() : string.Empty);
        }

        /// <summary>
        /// jQuery selector
        /// </summary>
        /// <returns>jQuery selector string</returns>
        public override string ToString()
        {
            return ToString(null);
        }
    }
}
