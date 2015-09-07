namespace Nest.Indexify.Contributors.Mapping
{
    public class FromAttributesIndexMappingContributor<TType> : IndexMappingContributor<TType> where TType : class
    {
        private readonly int _maxRecursion;
        public FromAttributesIndexMappingContributor(int maxRecursion = 0, int order = 0) : base(order)
        {
            _maxRecursion = maxRecursion;
        }

        protected override PutMappingDescriptor<TType> Mapping(PutMappingDescriptor<TType> descriptor)
        {
            return descriptor.MapFromAttributes(_maxRecursion);
        }
    }
}
