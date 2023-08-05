using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DisseminatedLetterExtensionRequestCollection class is designed to work with lists of instances of DisseminatedLetterExtensionRequest.
	/// </summary>
	public class DisseminatedLetterExtensionRequestCollection : BusinessCollectionBase<DisseminatedLetterExtensionRequest>
	{
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterExtensionRequestCollection class.
		/// </summary>
		public DisseminatedLetterExtensionRequestCollection() { }
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterExtensionRequestCollection class.
		/// </summary>
		public DisseminatedLetterExtensionRequestCollection(IList<DisseminatedLetterExtensionRequest> initialList) : base(initialList) { }
	}
}