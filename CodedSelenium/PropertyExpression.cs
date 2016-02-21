namespace CodedSelenium
{
    public class PropertyExpression
    {
        public PropertyExpression(string propertyName, string propertyValue)
        {
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
        }

        public PropertyExpression(string propertyName, string propertyValue, PropertyExpressionOperator propertyOperator)
            : this(propertyName, propertyValue)
        {
            this.PropertyOperator = propertyOperator;
        }

        public string PropertyName { get; set; }

        public PropertyExpressionOperator PropertyOperator { get; set; }

        public string PropertyValue { get; set; }
    }
}