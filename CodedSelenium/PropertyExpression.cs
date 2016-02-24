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

        public object Clone()
        {
            return (object)new PropertyExpression(this.PropertyName, this.PropertyValue, this.PropertyOperator);
        }

        public string PropertyName { get; set; }

        public PropertyExpressionOperator PropertyOperator { get; set; }

        public string PropertyValue { get; set; }
    }
}