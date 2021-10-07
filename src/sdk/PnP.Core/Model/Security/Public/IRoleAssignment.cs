namespace PnP.Core.Model.Security
{


    /// <summary>
    /// Defines a single role definition, including a name, description, and set of rights.
    /// </summary>
    [ConcreteType(typeof(RoleAssignment))]
    public interface IRoleAssignment : IDataModel<IRoleAssignment>, IDataModelGet<IRoleAssignment>, IDataModelLoad<IRoleAssignment>, IDataModelUpdate, IDataModelDelete, IQueryableDataModel
    {
        /// <summary>
        /// Gets or sets a value that specifies the base permissions for the role definition.
        /// </summary>
        public int PrincipalId { get; set; }

        /// <summary>
        /// Role definitions for this assignment
        /// </summary>
        public IRoleDefinitionCollection RoleDefinitions { get; }

        /// <summary>
        /// A special property used to add an asterisk to a $select statement
        /// </summary>
        public object All { get; }
    }
}