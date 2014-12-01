using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace Doc.Net.Lyn
{
    public class TypeMember
    {
        private readonly INamedTypeSymbol typeMember;

        public TypeMember(INamedTypeSymbol typeMember)
        {
            this.typeMember = typeMember;
            this.typeMember.Constructors.First().GetDocumentationCommentXml();
        }
    }
}
