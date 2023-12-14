using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SolrNet.Utils;

namespace SolrNet.Impl.ResponseParsers
{
    internal class FacetFunctionResponseParser<T> : ISolrAbstractResponseParser<T>
    {
        /// <summary>
        /// Parses facets from query response
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="results"></param>
        public void Parse(XDocument xml, AbstractSolrQueryResults<T> results)
        {
            var mainFacetNode = xml.Element("response")
                .Elements("lst")
                .FirstOrDefault(X.AttrEq("name", "facets"));
            if (mainFacetNode != null)
            {
                results.FacetFunctionValues = ParseFacetFunctionValues(mainFacetNode);
            }
        }

        /// <summary>
        /// Parses facet function results
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IDictionary<string, string> ParseFacetFunctionValues(XElement node)
        {
            var d = new Dictionary<string, string>();
            var facetQueries = node.Elements();
            string key = string.Empty, value = string.Empty;

            foreach (var fieldNode in facetQueries)
            {
                key = fieldNode.Attribute("name").Value;
                value = fieldNode.Value;
                d[key] = value;
            }
            return d;
        }
    }
}
