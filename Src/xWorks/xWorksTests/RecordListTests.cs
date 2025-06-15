// Copyright (c) 2022 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System.IO;
using System.Reflection;
using System.Xml;
using NUnit.Framework;
using SIL.FieldWorks.Common.FwUtils;
using SIL.FieldWorks.XWorks.LexEd;
using SIL.LCModel;
using SIL.LCModel.Infrastructure;
using System.Collections; // For ArrayList
using Moq; // Using Moq for ISilDataAccessManaged if needed, though direct mock might be simpler for get_VecItem
using SIL.FieldWorks.Filters; // For RecordSorter, RecordFilter
using SIL.LCModel.Core.KernelInterfaces; // For ICmObject
using System.Collections.Generic; // For List<T>

// ReSharper disable PossibleNullReferenceException (tests are expected to pass; ReSharper warnings are ugly)
namespace SIL.FieldWorks.XWorks
{
	// Mock IManyOnePathSortItem
	public class MockManyOnePathSortItem : IManyOnePathSortItem
	{
		public int Hvo { get; }
		public MockCmObject UnderlyingCmObject { get; }

		public MockManyOnePathSortItem(int hvo)
		{
			Hvo = hvo;
			UnderlyingCmObject = new MockCmObject { Hvo = hvo, StringProp = "HVO-" + hvo };
		}
		public MockManyOnePathSortItem(MockCmObject cmObject)
		{
			Hvo = cmObject.Hvo;
			UnderlyingCmObject = cmObject;
		}


		public int KeyObject => Hvo;
		public ICmObject KeyObjectUsing(LcmCache cache) => UnderlyingCmObject;
		public int RootObjectHvo => Hvo;
		public ICmObject RootObjectUsing(LcmCache cache) => UnderlyingCmObject;
		public int PathObject(int index) => 0;
		public int PathLength => 0;
		public bool IsValid => true;
		public void AssertValid() { }
		public void Update(int hvoRoot) { }
		public void Dispose() { }
	}

	// Mock ICmObject (simplified)
	public class MockCmObject : ICmObject
	{
		public int Hvo { get; set; }
		public string StringProp { get; set; }
		public int IntProp { get; set; }

		// Implement minimal ICmObject members.
		public int ClassID => 0;
		public bool IsDirty => false;
		public bool IsNew => false;
		public ICmObject Owner => null;
		public int OwningFlid => 0;
		public int OwnOrd => 0;
		public Guid Guid => Guid.NewGuid();
		public ISilDataAccess SilDataAccess => null;
		public LcmCache Cache => null;
		public void SetDirty() { }
		public void SetNew(bool isNew) { }
		public string ShortName => "MockCmObject-" + Hvo;
		public string SortKey { get; set; }
		public int SortKeyWs { get; set; }
		public ICmObject RootObject => this;
		public IServiceProvider ServiceProvider => null;
		public void CheckDisposed() { }
		public bool IsDisposed => false;
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public void Dispose() { }
		public string GetPropertyXml(string propertyName, bool convertGuidsToLocalIds) { return string.Empty; }
		public string GetMultiStringAlt(int flid, int ws, bool fallBackToAnalysisDefault) { return string.Empty; }
		public void SetMultiStringAlt(int flid, int ws, string value) { }
		public string BestAnalysisVernacularAlternative(int flid) { return string.Empty; }
		public string BestAnalysisAlternative(int flid) { return string.Empty; }
		public string BestVernacularAlternative(int flid) { return string.Empty; }
		public bool TryGetObject(int hvo, out ICmObject obj) { obj = null; return false; }
		public Guid GetGuid() { return Guid; }
		public void SetGuid(Guid guid) { }
		public int GetActualPropertyType(int flid) { return 0; }
		public IEnumerable<int> GetVectorProperty(int flid, bool includeGhosts) { return new List<int>(); }
		public int GetVectorSize(int flid) { return 0; }
		public bool IsValidObject() { return true; }
		public bool IsValidOrphan() { return true; }
		public string GetShortName(int wsUi) { return ShortName; }
		public void Dump(TextWriter writer, DumpStyle style, HashSet<int> objectsToDump) { }
		public int GetOwningDepth() { return 0; }
		public ICmObject GetOrSetOwner(int hvoNewOwner, int flid, int ord) { return null; }
		public void SetOwner(ICmObject newOwner, int flid, int ord) { }
		public void SetOwnOrd(int owningFlid, int newOrd) { }
		public ICmObject Clone() { return null; }
		public void SetIntProperty(int flid, int value) { }
		public void SetStringProperty(int flid, string value, int ws) { }
		public void DeleteObjProperty(int flid) { }
		public void DeleteObjProperty(int flid, int hvoObj) { }
		public void InsertObjVectorProperty(int flid, int hvoObj, int ord) { }
		public void SetObjProperty(int flid, int hvo) { }
		public void SetTextProperty(int flid, SIL.LCModel.Core.Text.IStText value) { }
		public void SetUnicodeProperty(int flid, string value) { }
		public string GetUnicodeProperty(int flid) { return string.Empty; }
		public SIL.LCModel.Core.Text.IStText GetTextProperty(int flid) { return null; }
		public int GetObjProperty(int flid) { return 0; }
		public int GetIntProperty(int flid) { return 0; }
		public bool GetBoolProperty(int flid) { return false; }
		public void SetBoolProperty(int flid, bool val) { }
		public DateTime GetDateProperty(int flid) { return DateTime.MinValue; }
		public void SetDateProperty(int flid, DateTime val) { }
		public byte[] GetBinaryProperty(int flid) { return null; }
		public void SetBinaryProperty(int flid, byte[] val) { }
		public bool IsNull(int flid) { return false; }
		public bool IsValidObjectId(int hvo) { return false; }
		public string GetStringProperty(int flid, int ws) { return string.Empty; }
		public int GetStringPropVernWs(int flid) { return 0; }
		public int GetStringPropAnalWs(int flid) { return 0; }
		public void SetDummyReferences(ICmObject owner) { }
		public string GetName(int ws) { return string.Empty; }
		public string FullName { get { return string.Empty; } }
		public string GetPropertyLabel(int flid) { return string.Empty; }
		public bool IsValidProperty(int flid, bool fStrict) { return false; }
		public bool IsAtomicProp(int flid) { return false; }
		public bool IsCollectionProp(int flid) { return false; }
		public bool IsObjectProp(int flid) { return false; }
		public bool IsVectorProp(int flid) { return false; }
		public bool IsProtected(int flid) { return false; }
		public bool IsReadOnly(int flid) { return false; }
		public bool IsMultiStringProp(int flid) { return false; }
		public bool IsBigStringProp(int flid) { return false; }
		public bool IsBigTextProp(int flid) { return false; }
		public bool IsTextProp(int flid) { return false; }
		public bool IsUnicodeProp(int flid) { return false; }
		public bool IsIntegerProp(int flid) { return false; }
		public bool IsGuidProp(int flid) { return false; }
		public bool IsDateProp(int flid) { return false; }
		public bool IsBinaryProp(int flid) { return false; }
		public bool IsBooleanProp(int flid) { return false; }
		public bool IsGenDateProp(int flid) { return false; }
		public int GetPropClass(int flid) { return 0; }
		public bool IsCustomProperty(int flid) { return false; }
		public void MakeNewCustomField(string fieldName, int type, int ws, bool isWsSpecific) { }
		public void DeleteCustomField(int flid) { }
		public void MakeNewCustomField(string fieldName, int type, int ws, bool isWsSpecific, string listRootGuid) { }
		public int GetFieldType(int flid) { return 0; }
		public string GetNameForCustomProperty(int flid) { return string.Empty; }
		public int GetWsForCustomProperty(int flid) { return 0; }
		public bool GetIsWsSpecificForCustomProperty(int flid) { return false; }
		public string GetListRootGuidForCustomProperty(int flid) { return string.Empty; }
		public bool GetCanDeleteCustomProperty(int flid) { return false; }
		public bool GetIsCustomPropWritable(int flid) { return false; }
		public bool IsCustomFieldUsed(int flid) { return false; }
		public bool IsCustomFieldReadOnly(int flid) { return false; }
		public bool IsCustomFieldProtected(int flid) { return false; }
		public void SetCustomPropLabel(int flid, string label, int ws) { }
		public string GetCustomPropLabel(int flid, int ws) { return string.Empty; }
		public void SetCustomPropDescription(int flid, string description, int ws) { }
		public string GetCustomPropDescription(int flid, int ws) { return string.Empty; }
		public void SetCustomPropHelp(int flid, string helpString, int ws) { }
		public string GetCustomPropHelp(int flid, int ws) { return string.Empty; }
		public void SetCustomPropGenericName(int flid, string genericName, int ws) { }
		public string GetCustomPropGenericName(int flid, int ws) { return string.Empty; }
		public void SetCustomPropMin(int flid, string min) { }
		public string GetCustomPropMin(int flid) { return string.Empty; }
		public void SetCustomPropMax(int flid, string max) { }
		public string GetCustomPropMax(int flid) { return string.Empty; }
		public void SetCustomPropStringMax(int flid, int max) { }
		public int GetCustomPropStringMax(int flid) { return 0; }
		public void SetCustomPropBigStringDef(int flid, string def) { }
		public string GetCustomPropBigStringDef(int flid) { return string.Empty; }
		public void SetCustomPropBigStringUserDef(int flid, string userDef) { }
		public string GetCustomPropBigStringUserDef(int flid) { return string.Empty; }
		public bool GetCustomPropBigStringIsMulti(int flid) { return false; }
		public string GetCustomPropBigStringValidation(int flid) { return string.Empty; }
		public void SetCustomPropBigStringValidation(int flid, string validation) { }
		public string GetCustomPropUserPrompt(int flid) { return string.Empty; }
		public void SetCustomPropUserPrompt(int flid, string prompt) { }
		public bool GetCustomPropIsSecure(int flid) { return false; }
		public void SetCustomPropIsSecure(int flid, bool isSecure) { }
		public bool IsSecureCustomProperty(int flid) { return false; }
		public string GetCustomPropRegExp(int flid) { return string.Empty; }
		public void SetCustomPropRegExp(int flid, string regExp) { }
		public string[] GetCustomPropChoices(int flid) { return null; }
		public void SetCustomPropChoices(int flid, string[] choices) { }
		public bool IsValidValueForCustomList(int flid, string value) { return false; }
		public int[] GetCustomFields(int clsid, bool fIncludeInherited, int fieldTypeFilter) { return null; }
		public void ReorderCustomField(int flid, int flidBefore) { }
		public void SetCustomPropVisibility(int flid, bool isVisible) { }
		public bool GetCustomPropVisibility(int flid) { return false; }
		public string GetCustomPropHelpFile(int flid) { return string.Empty; }
		public void SetCustomPropHelpFile(int flid, string helpFile) { }
		public void SetCustomPropHelpTopic(int flid, string helpTopic) { }
		public string GetCustomPropHelpTopic(int flid) { return string.Empty; }
		public bool IsSameOrSubclassOf(int clsidOther) { return false; }
		public bool IsContainedIn(int hvo) { return false; }
		public ICmObject GetAncestor(int clsid) { return null; }
		public ICmObject GetAncestor(int[] clsid) { return null; }
		public ICmObject GetOuterCmObject() { return null; }
		public void GetPossibilities(int flid, SIL.ObjectModel.Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
		public bool IsValidValue(int flid, int hvo) { return false; }
		public bool IsValidValue(int flid, string value, int ws) { return false; }
		public bool IsValidValue(int flid, string value) { return false; }
		public bool CanSetProperty(int flid) { return false; }
		public string GetPropertyLabel(int flid, int wsUi) { return string.Empty; }
		public void GetIndirectPossibilities(int flid, SIL.ObjectModel.Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
		public void GetDirectPossibilities(int flid, SIL.ObjectModel.Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
		public bool IsValidObject(int flid, int hvo) { return false; }
		public bool IsValidObject(int flid, int hvo, bool fStrict) { return false; }
		public string GetNameForWs(int ws) { return string.Empty; }
		public int GetDefaultAnalWs() { return 0; }
		public int GetDefaultUserWs() { return 0; }
		public int GetDefaultVernWs() { return 0; }
		public bool RemoveSdaNotification(IVwNotifyChange nc) { return false; }
		public void AddSdaNotification(IVwNotifyChange nc) { }
		public string GetPathToCmObject(int hvoTarget) { return string.Empty; }
		public bool IsStringPropEmpty(int flid) { return false; }
		public bool IsComplexForm(int flid) { return false; }
		public bool IsEncrypted(int flid) { return false; }
		public string GetPropertyType(int flid) { return string.Empty; }
		public string GetPropertyClassName(int flid) { return string.Empty; }
		public string GetPropertySig(int flid) { return string.Empty; }
		public int GetFieldId(string fieldName, bool fMustExist) { return 0; }
		public string GetFieldName(int flid) { return string.Empty; }
		public bool IsOwnedBy(int hvoPossibleOwner) { return false; }
		public bool FieldExists(int flid) { return false; }
		public string PropNameFromFlid(int flid) { return string.Empty; }
		public bool CanCreateCustomFields(int clsid) { return false; }
		public int GetNamedCustomFieldId(int clsid, string fieldName, int type, int ws, bool isWsSpecific) { return 0; }
		public void SetCustomPropVisibility(int flid, bool isVisible, bool isOverride) { }
		public void SetCustomPropHelpFile(int flid, string helpFile, bool isOverride) { }
		public void SetCustomPropHelpTopic(int flid, string helpTopic, bool isOverride) { }
		public void SetCustomPropUserPrompt(int flid, string prompt, bool isOverride) { }
		public int GetFieldWs(int flid) { return 0; }
		public bool IsFieldWsSpecific(int flid) { return false; }
		public string GetListRootGuid(int flid) { return string.Empty; }
		public bool TryGetGuidProperty(int flid, out Guid guid) { guid = Guid.Empty; return false; }
		public void SetGuidProperty(int flid, Guid guid) { }
		public bool IsGuidEmpty(int flid) { return false; }
		public System.Collections.Generic.IEnumerable<int> GetMultiIntProperty(int flid) { return null; }
		public void SetMultiIntProperty(int flid, System.Collections.Generic.IEnumerable<int> values) { }
		public void DeleteMultiIntProperty(int flid, int value) { }
		public bool IsMultiIntPropEmpty(int flid) { return false; }
		public int GetMultiIntPropCount(int flid) { return 0; }
		public string GetXmlPathToCmObject(int hvoTarget) { return string.Empty; }
		public int GetClassId(string className) { return 0; }
		public string GetClassName(int clsid) { return string.Empty; }
		public string GetFullClassName(int clsid) { return string.Empty; }
		public string GetFieldSig(int flid) { return string.Empty; }
		public int GetFieldClsid(int flid) { return 0; }
		public int GetFieldDstClsid(int flid) { return 0; }
		public int GetFieldAtomicOrCustomType(int flid) { return 0; }
		public bool IsFieldSystemInternal(int flid) { return false; }
		public bool IsFieldUserVisible(int flid) { return false; }
		public string GetFieldLabel(int flid, int wsUi) { return string.Empty; }
		public string GetFieldDescription(int flid, int wsUi) { return string.Empty; }
		public string GetFieldHelpString(int flid, int wsUi) { return string.Empty; }
		public string GetFieldGenericName(int flid, int wsUi) { return string.Empty; }
		public string GetFieldMin(int flid) { return string.Empty; }
		public string GetFieldMax(int flid) { return string.Empty; }
		public int GetFieldStringMax(int flid) { return 0; }
		public string GetFieldBigStringDef(int flid) { return string.Empty; }
		public string GetFieldBigStringUserDef(int flid) { return string.Empty; }
		public bool GetFieldBigStringIsMulti(int flid) { return false; }
		public string GetFieldBigStringValidation(int flid) { return string.Empty; }
		public string GetFieldUserPrompt(int flid) { return string.Empty; }
		public bool GetFieldIsSecure(int flid) { return false; }
		public string GetFieldRegExp(int flid) { return string.Empty; }
		public string[] GetFieldChoices(int flid) { return null; }
		public bool IsValidValueForFieldList(int flid, string value) { return false; }
		public int[] GetFields(int clsid, bool fIncludeInherited, int fieldTypeFilter) { return null; }
		public bool FieldCanReorderField(int flid) { return false; }
		public bool FieldGetCustomPropVisibility(int flid) { return false; }
		public string FieldGetCustomPropHelpFile(int flid) { return string.Empty; }
		public string FieldGetCustomPropHelpTopic(int flid) { return string.Empty; }
		public string FieldGetOwnClsName(int flid) { return string.Empty; }
		public int FieldGetOwnClsId(int flid) { return 0; }
	}


	public class MockRecordSorter : RecordSorter
	{
		public bool SortCalled { get; private set; }
		public bool MergeIntoCalled { get; private set; }
		public ArrayList MergedItems { get; private set; }
		public LcmCache CacheUsed { get; private set; }
		public ISilDataAccess DataAccessUsed { get; private set; }

		public override LcmCache Cache { set { CacheUsed = value; base.Cache = value; } }
		public override ISilDataAccess DataAccess { set { DataAccessUsed = value; base.DataAccess = value; } }


		public override void Sort(ArrayList records)
		{
			SortCalled = true;
			// Simple HVO based sort for testing
			records.Sort(Comparer ?? ComparerInstance);
		}

		public override void MergeInto(ArrayList records, ArrayList newRecords)
		{
			MergeIntoCalled = true;
			MergedItems = new ArrayList(newRecords); // Capture what was passed to be merged
			foreach (var item in newRecords)
			{
				// Simple HVO based merge for testing (inserting into sorted position)
				int i = 0;
				for (; i < records.Count; i++)
				{
					if ((Comparer ?? ComparerInstance).Compare(item, records[i]) < 0)
						break;
				}
				records.Insert(i, item);
			}
		}

		protected internal override IComparer getComparer()
		{
			return ComparerInstance;
		}

		private static readonly HvoComparer ComparerInstance = new HvoComparer();
		private class HvoComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				var itemX = (IManyOnePathSortItem)x;
				var itemY = (IManyOnePathSortItem)y;
				return itemX.RootObjectHvo.CompareTo(itemY.RootObjectHvo);
			}
		}

		public override void CollectItems(int hvo, ArrayList collector)
		{
			collector.Add(new MockManyOnePathSortItem(hvo));
		}
	}

	[TestFixture]
	public class RecordListTests : AllReversalEntriesRecordListTestBase
	{
		protected RecordClerk m_clerk;
		protected AllReversalEntriesRecordListForTests m_list;
		protected int m_wsEn;

		[OneTimeSetUp]
		public override void FixtureInit()
		{
			base.FixtureInit();
			CreateClerkAndList();
			m_propertyTable.SetProperty("ActiveClerk", m_clerk, false);
			Cache.ProjectId.Path = Path.Combine(FwDirectoryFinder.SourceDirectory, "xWorks/xWorksTests/TestData/");
			m_wsEn = Cache.DefaultAnalWs;
		}

		[SetUp]
		public override void TestSetup()
		{
			base.TestSetup();
			m_clerk.ActivateUI(false);
			m_list.ResetReloadCount();
		}

		private void CreateClerkAndList()
		{
			// ReSharper disable StringLiteralTypo - copied from configuration xml
			const string entryClerk = @"<?xml version='1.0' encoding='UTF-8'?>
			<root>
				<clerks>
					<clerk id='AllReversalEntries'>
						<dynamicloaderinfo assemblyPath='xWorksTests.dll' class='SIL.FieldWorks.XWorks.ReversalEntryClerkForListTests'/>
						<recordList owner='ReversalIndex' property='AllEntries'>
							<dynamicloaderinfo assemblyPath='xWorksTests.dll' class='SIL.FieldWorks.XWorks.AllReversalEntriesRecordListForTests'/>
						</recordList>
					</clerk>
				</clerks>
				<tools>
					<tool label='Reversal Indexes' value='reversalToolEditComplete' icon='SideBySideView'>
						<control>
							<dynamicloaderinfo assemblyPath='xWorks.dll' class='SIL.FieldWorks.XWorks.XhtmlDocView'/>
							<parameters area='lexicon' clerk='AllReversalEntries' altTitleId='ReversalIndexEntry-Plural' persistContext='Reversal'
								backColor='White' layout='' layoutProperty='ReversalIndexPublicationLayout' layoutSuffix='Preview' editable='false'
								configureObjectName='ReversalIndex'/>
						</control>
					</tool>
				</tools>
			</root>";
			var doc = new XmlDocument();
			doc.LoadXml(entryClerk);
			var clerkNode = doc.SelectSingleNode("//tools/tool[@label='Reversal Indexes']//parameters[@area='lexicon']");
			m_clerk = RecordClerkFactory.CreateClerk(m_mediator, m_propertyTable, clerkNode, false);
			m_clerk.Init(m_mediator, m_propertyTable, clerkNode);
			m_list = (AllReversalEntriesRecordListForTests)m_clerk.GetType().GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(m_clerk);
			m_list.ResetReloadCount();
		}

		/// <remarks>called by OneTimeTearDown</remarks>
		protected override void TearDown()
		{
			base.TearDown();
			m_clerk?.Dispose();
			m_clerk = null;
		}

		/// <summary>
		/// Finish loading the list for tests that need it, then reset the reload count
		/// </summary>
		protected void FinishLoadingAndAssertPreconditions()
		{
			// SetSuppressingLoadList and ReloadList separately, since other tests may leave residue.
			m_list.SetSuppressingLoadList(false);
			m_list.ReloadList();
			m_list.CurrentIndex = 0;
			m_list.ResetReloadCount();

			Assert.That(m_list.OwningObject, Is.Not.Null);
			Assert.That(m_list.CurrentIndex, Is.EqualTo(0));
			Assert.That(m_list.HvoCurrent, Is.GreaterThan(0));
		}

		[Test]
		public void Reload_NoInsertionsOrDeletions_NoReloads()
		{
			FinishLoadingAndAssertPreconditions();

			// SUT
			m_list.ReloadList(0, 0, 0);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(0));
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(0));
		}

		[Test]
		public void Reload_OneExistingItem_Reloads()
		{
			FinishLoadingAndAssertPreconditions();
			Assert.That(m_list.ItemCount, Is.EqualTo(1), "One item should have been added by per-test setup");

			// SUT
			m_list.ReloadList(0, 1, 1);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(1));
		}

		[Test]
		public void Reload_NoCurrentObject_Reloads()
		{
			AddSomeReversalEntries();
			FinishLoadingAndAssertPreconditions();
			m_list.HvoCurrent = 0;

			// SUT
			m_list.ReloadList(0, 0, 0);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(1));
		}

		[Test]
		public void Reload_NoOwningObject_Reloads()
		{
			AddSomeReversalEntries();
			FinishLoadingAndAssertPreconditions();
			var owningObject = m_list.ClearOwningObjectNoSideEffects();
			try
			{
				// SUT
				m_list.ReloadList(0, 0, 0);

				Assert.That(m_list.ReloadCallCount, Is.EqualTo(1));
			}
			finally
			{
				m_list.OwningObject = owningObject;
			}
		}

		[Test]
		public void Reload_OneItemToUpdateOfMany_UsesQuickUpdate()
		{
			AddSomeReversalEntries();
			FinishLoadingAndAssertPreconditions();

			// SUT
			m_list.ReloadList(0, 1, 1);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(0));
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(1));
		}

		[Test]
		public void Reload_ManyChanges_Reloads()
		{
			AddSomeReversalEntries();
			FinishLoadingAndAssertPreconditions();

			// SUT
			m_list.ReloadList(1, 2, 3);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(1));
		}

		[Test]
		public void Reload_AddedItems_Reloads([Values(1, 3)] int cAdded)
		{
			FinishLoadingAndAssertPreconditions();

			// SUT (committing the undo task should trigger a reload)
			AddSomeReversalEntries(cAdded);

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(1), "Inserting item(s) requires reloading the list");
			Assert.That(m_list.ItemCount, Is.EqualTo(1 + cAdded), "List should contain the new items");
		}

		[Test]
		public void Reload_DeletedSomeItems_UsesQuickUpdate([Values(1, 3)] int cAdded)
		{
			var deletable = AddSomeReversalEntries(cAdded);
			AddSomeReversalEntries(6); // these are not deletable
			FinishLoadingAndAssertPreconditions();

			// SUT (committing the undo task should trigger a reload)
			NonUndoableUnitOfWorkHelper.Do(Cache.ActionHandlerAccessor, () =>
			{
				deletable.ForEach(e => e.Delete());
			});

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(0), "Simply deleting a few items should't require the entire list to be reloaded");
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(1), "Removing items should trigger a reloaded event");
			Assert.That(m_list.ItemCount, Is.EqualTo(7), "Deletable items should have been deleted");
		}

		[Test]
		public void Reload_DeletedHalfOfItems_Reloads()
		{
			var deletable = AddSomeReversalEntries(3);
			AddSomeReversalEntries(2);
			FinishLoadingAndAssertPreconditions();

			// SUT (committing the undo task should trigger a reload)
			NonUndoableUnitOfWorkHelper.Do(Cache.ActionHandlerAccessor, () =>
			{
				deletable.ForEach(e => e.Delete());
			});

			Assert.That(m_list.ReloadCallCount, Is.EqualTo(1), "Deleting half the items should reload the whole list");
			Assert.That(m_list.ItemCount, Is.EqualTo(3), "Deletable items should have been deleted");
		}

		[Test]
		public void ListLoadingSuppressed()
		{
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.True, "Lists start with a pending reload");
			Assert.That(m_list.ListLoadingSuppressed, Is.True, "Lists start with loading suppressed");
			// Set to false w/o the side effect of reloading
			m_list.SetSuppressingLoadList(false);
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.True, "Reload should still be pending");
			Assert.That(m_list.ReloadCallCount, Is.EqualTo(0), "Shouldn't have reloaded");
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(0), "How did that happen?");

			m_list.ListLoadingSuppressed = true;
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.False, "Haven't tried to reload since suppressing");
			m_list.ReloadList();
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.True, "Requested Load While Suppressed");
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(0), "Shouldn't have actually reloaded");

			m_list.ListLoadingSuppressed = false;
			Assert.That(m_list.ReloadCallCount, Is.EqualTo(2), "Setting suppression to false should have triggered the suppressed reload");
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(1), "Setting suppression to false should have triggered the suppressed reload");
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.False, "The requested reload has completed; the request can be forgotten");

			m_list.RequestedLoadWhileSuppressed = true;
			m_list.ReloadList();
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(2), "should have reloaded as requested");
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.False, "Reload should have cleared the reload requested");
		}

		[Test]
		public void ListLoadingSuppressedByInactiveClerk()
		{
			m_clerk.BecomeInactive();
			Assert.That(m_clerk.IsActiveInGui, Is.False, "Clerk starts inactive");
			m_list.SetSuppressingLoadList(false);
			m_list.RequestedLoadWhileSuppressed = false;
			m_list.ReloadList();
			Assert.That(m_list.RequestedLoadWhileSuppressed, Is.True, "Requested Load While Inactive");
			Assert.That(m_list.ReloadEventCount, Is.EqualTo(0), "Shouldn't have actually reloaded");
		}
	}

	// ReSharper disable once UnusedMember.Global (created by reflection)
	public class ReversalEntryClerkForListTests : ReversalEntryClerk
	{
		public override void ActivateUI(bool useRecordTreeBar, bool updateStatusBar = true) => m_fIsActiveInGui = true;

		/// <returns>false: we didn't even try (w/o extra setup, trying crashes)</returns>
		protected override bool TryRestoreSorter(XmlNode clerkConfiguration, LcmCache cache)
		{
			return false;
		}
	}

	public class AllReversalEntriesRecordListForTests : AllReversalEntriesRecordList
	{
		public ICmObject ClearOwningObjectNoSideEffects()
		{
			var oldOwner = m_owningObject;
			m_owningObject = null;
			return oldOwner;
		}

		public int HvoCurrent
		{
			get => m_hvoCurrent;
			set => m_hvoCurrent = value;
		}

		/// <summary>Number of items in the list</summary>
		public int ItemCount => VirtualListPublisher.get_VecSize(m_owningObject.Hvo, m_flid);
		/// <summary>Number of times the ReloadList method was called, even if reloading was suppressed</summary>
		public int ReloadCallCount { get; private set; }
		/// <summary>Number of DoneReload events that have fired (includes full reloads and single-item "updates" and "replacements"</summary>
		public int ReloadEventCount { get; private set; }

		public void ResetReloadCount() => ReloadCallCount = ReloadEventCount = 0; // TODO (Hasso) 2022.08: call this from Init

		public AllReversalEntriesRecordListForTests()
		{
			DoneReload += (sender, args) => { ReloadEventCount++; };
		}

		public override void ReloadListSynchronous()
		{
			ReloadCallCount++;
			base.ReloadListSynchronous();
		}

		// New field for checking ListChanged event
		private ListChangedEventArgs _listChangedEventArgs;
		private bool _listChangedEventFired;

		private void HandleListChanged(object sender, ListChangedEventArgs e)
		{
			_listChangedEventFired = true;
			_listChangedEventArgs = e;
		}


		// Helper to set private/protected members for testing
		private void SetRecordListSorter(RecordList list, RecordSorter sorter)
		{
			var sorterField = typeof(RecordList).GetField("m_sorter", BindingFlags.Instance | BindingFlags.NonPublic);
			sorterField.SetValue(list, sorter);
		}
		private void SetRecordListFilter(RecordList list, RecordFilter filter)
		{
			var filterField = typeof(RecordList).GetField("m_filter", BindingFlags.Instance | BindingFlags.NonPublic);
			filterField.SetValue(list, filter);
		}

		private ArrayList GetSortedObjects(RecordList list)
		{
			var sortedObjectsField = typeof(RecordList).GetField("m_sortedObjects", BindingFlags.Instance | BindingFlags.NonPublic);
			return (ArrayList)sortedObjectsField.GetValue(list);
		}
		private void SetSortedObjects(RecordList list, ArrayList sortedObjects)
		{
			var sortedObjectsField = typeof(RecordList).GetField("m_sortedObjects", BindingFlags.Instance | BindingFlags.NonPublic);
			sortedObjectsField.SetValue(list, sortedObjects);
		}


		[Test]
		public void ReloadList_PureAddition_WithSorter_CallsMergeInto()
		{
			FinishLoadingAndAssertPreconditions(); // Ensures m_list is set up
			var initialSortedObjects = new ArrayList(GetSortedObjects(m_list));
			int originalCount = initialSortedObjects.Count;

			var mockSorter = new MockRecordSorter();
			SetRecordListSorter(m_list, mockSorter);

			// Simulate new items being available via VirtualListPublisher
			// The actual AddSomeReversalEntries adds to Cache; ReloadList will pick them up.
			// For this test, we want to control exactly what get_VecItem returns.
			// We'll mock the VirtualListPublisher for m_list.
			var mockSda = new Mock<ISilDataAccessManaged>();
			var owningObjForTest = new MockCmObject { Hvo = 1000 };
			SetField(m_list, "m_owningObject", owningObjForTest); // Ensure m_list.OwningObject is our mock
			SetField(m_list, "m_flid", 1); // Ensure m_flid is set

			int ivMin = originalCount; // Adding after existing items
			int cvIns = 2;
			var newHvo1 = 901;
			var newHvo2 = 902;
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, It.Is<int>(i => i == ivMin))).Returns(newHvo1);
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, It.Is<int>(i => i == ivMin + 1))).Returns(newHvo2);
			// get_VecSize is not directly used by this specific ReloadList overload's optimization path,
			// but MakeItemsFor (called by the optimization) might use it if it needs to create real CmObjects.
			// The mock Sorter's CollectItems creates MockManyOnePathSortItem directly.

			// Replace the list's publisher with our mock
			var publisherField = typeof(RecordList).GetField("m_publisher", BindingFlags.Instance | BindingFlags.NonPublic);
			Assert.IsNotNull(publisherField, "m_publisher field not found.");
			var originalPublisher = publisherField.GetValue(m_list); // For restoring later if necessary
			publisherField.SetValue(m_list, mockSda.Object);


			// SUT
			m_list.ReloadList(ivMin, cvIns, 0); // cvDel = 0

			Assert.IsTrue(mockSorter.MergeIntoCalled, "MergeInto should be called on the sorter.");
			Assert.IsFalse(mockSorter.SortCalled, "Sort should NOT be called on the sorter for pure addition.");
			Assert.IsNotNull(mockSorter.MergedItems, "MergedItems should have been captured.");
			Assert.AreEqual(cvIns, mockSorter.MergedItems.Count, "Correct number of new items should be merged.");
			Assert.AreEqual(newHvo1, ((IManyOnePathSortItem)mockSorter.MergedItems[0]).RootObjectHvo);
			Assert.AreEqual(newHvo2, ((IManyOnePathSortItem)mockSorter.MergedItems[1]).RootObjectHvo);

			var finalSortedObjects = GetSortedObjects(m_list);
			Assert.AreEqual(originalCount + cvIns, finalSortedObjects.Count, "SortedObjects should contain original and new items.");
			// Specific order depends on MergeInto mock logic, here it's simple append due to HVOs
			bool foundNew1 = false, foundNew2 = false;
			foreach (IManyOnePathSortItem item in finalSortedObjects)
			{
				if (item.RootObjectHvo == newHvo1) foundNew1 = true;
				if (item.RootObjectHvo == newHvo2) foundNew2 = true;
			}
			Assert.IsTrue(foundNew1 && foundNew2, "New items should be in the final sorted list.");

			// Restore original publisher if necessary, though for this test structure it might not matter
			publisherField.SetValue(m_list, originalPublisher);
		}


		[Test]
		public void ReloadList_PureAddition_NoSorter_AddsToEnd()
		{
			FinishLoadingAndAssertPreconditions();
			var initialSortedObjects = new ArrayList(GetSortedObjects(m_list));
			int originalCount = initialSortedObjects.Count;
			initialSortedObjects.Add(new MockManyOnePathSortItem(10)); // Add an item to ensure list is not empty

			SetSortedObjects(m_list, initialSortedObjects); // Set initial state
			originalCount = initialSortedObjects.Count;


			SetRecordListSorter(m_list, null); // No sorter

			var mockSda = new Mock<ISilDataAccessManaged>();
			var owningObjForTest = new MockCmObject { Hvo = 1000 };
			SetField(m_list, "m_owningObject", owningObjForTest);
			SetField(m_list, "m_flid", 1);

			int ivMin = originalCount;
			int cvIns = 2;
			var newHvo1 = 901;
			var newHvo2 = 902;
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, ivMin)).Returns(newHvo1);
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, ivMin + 1)).Returns(newHvo2);
			// Mock MakeItemsFor (which calls CollectItems on sorter if present, or default if not)
			// Since sorter is null, RecordList.MakeItemsFor will add new ManyOnePathSortItem(hvo, null, null)
			// This is handled by default logic.

			var publisherField = typeof(RecordList).GetField("m_publisher", BindingFlags.Instance | BindingFlags.NonPublic);
			var originalPublisher = publisherField.GetValue(m_list);
			publisherField.SetValue(m_list, mockSda.Object);

			// SUT
			m_list.ReloadList(ivMin, cvIns, 0);

			var finalSortedObjects = GetSortedObjects(m_list);
			Assert.AreEqual(originalCount + cvIns, finalSortedObjects.Count, "Final list should have original + new items.");
			Assert.AreEqual(newHvo1, ((IManyOnePathSortItem)finalSortedObjects[originalCount]).RootObjectHvo, "First new item should be at the end.");
			Assert.AreEqual(newHvo2, ((IManyOnePathSortItem)finalSortedObjects[originalCount + 1]).RootObjectHvo, "Second new item should be after first new item.");

			publisherField.SetValue(m_list, originalPublisher);
		}


		[Test]
		public void ReloadList_PureAddition_WithFilter_MergesFilteredItems()
		{
			FinishLoadingAndAssertPreconditions();
			var initialSortedObjects = new ArrayList(GetSortedObjects(m_list));
			int originalCount = initialSortedObjects.Count;

			var mockSorter = new MockRecordSorter();
			SetRecordListSorter(m_list, mockSorter);

			var mockFilter = new Mock<RecordFilter>();
			// Filter accepts items with HVO > 901 (i.e., newHvo2 but not newHvo1)
			mockFilter.Setup(f => f.Accept(It.IsAny<IManyOnePathSortItem>()))
				.Returns<IManyOnePathSortItem>(item => item.RootObjectHvo > 901);
			SetRecordListFilter(m_list, mockFilter.Object);


			var mockSda = new Mock<ISilDataAccessManaged>();
			var owningObjForTest = new MockCmObject { Hvo = 1000 };
			SetField(m_list, "m_owningObject", owningObjForTest);
			SetField(m_list, "m_flid", 1);


			int ivMin = originalCount;
			int cvIns = 2;
			var newHvo1 = 901; // Will be filtered out
			var newHvo2 = 902; // Will be kept
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, ivMin)).Returns(newHvo1);
			mockSda.Setup(x => x.get_VecItem(owningObjForTest.Hvo, 1, ivMin + 1)).Returns(newHvo2);


			var publisherField = typeof(RecordList).GetField("m_publisher", BindingFlags.Instance | BindingFlags.NonPublic);
			var originalPublisher = publisherField.GetValue(m_list);
			publisherField.SetValue(m_list, mockSda.Object);


			m_list.ReloadList(ivMin, cvIns, 0);

			Assert.IsTrue(mockSorter.MergeIntoCalled, "MergeInto should be called.");
			Assert.IsNotNull(mockSorter.MergedItems, "MergedItems should have been captured.");
			Assert.AreEqual(1, mockSorter.MergedItems.Count, "Only one item (non-filtered) should be merged.");
			Assert.AreEqual(newHvo2, ((IManyOnePathSortItem)mockSorter.MergedItems[0]).RootObjectHvo, "The non-filtered item HVO should be 902.");

			var finalSortedObjects = GetSortedObjects(m_list);
			Assert.AreEqual(originalCount + 1, finalSortedObjects.Count, "SortedObjects should contain original items + 1 new filtered item.");

			publisherField.SetValue(m_list, originalPublisher);
		}


		[Test]
		public void ReloadList_NoChange_NoAction_WhenOptimizationApplies()
		{
			FinishLoadingAndAssertPreconditions();
			var initialSortedObjects = new ArrayList(GetSortedObjects(m_list));
			int originalCount = initialSortedObjects.Count;

			var mockSorter = new MockRecordSorter();
			SetRecordListSorter(m_list, mockSorter);

			// SUT
			m_list.ReloadList(originalCount, 0, 0); // ivMin = originalCount, cvIns = 0, cvDel = 0

			Assert.IsFalse(mockSorter.SortCalled, "Sort should not be called.");
			Assert.IsFalse(mockSorter.MergeIntoCalled, "MergeInto should not be called.");
			var finalSortedObjects = GetSortedObjects(m_list);
			Assert.AreEqual(originalCount, finalSortedObjects.Count, "SortedObjects count should be unchanged.");
			// Could also verify content if concerned about subtle changes.
		}

		// Helper to set private/protected instance fields using reflection
		private void SetField(object instance, string fieldName, object value)
		{
			var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (field == null)
			{
				// Try searching in base types
				var baseType = instance.GetType().BaseType;
				while (baseType != null)
				{
					field = baseType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
					if (field != null) break;
					baseType = baseType.BaseType;
				}
			}
			Assert.IsNotNull(field, $"Field '{fieldName}' not found in {instance.GetType()} or its base types.");
			field.SetValue(instance, value);
		}

		private T GetField<T>(object instance, string fieldName)
		{
			var field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (field == null)
			{
				var baseType = instance.GetType().BaseType;
				while (baseType != null)
				{
					field = baseType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
					if (field != null) break;
					baseType = baseType.BaseType;
				}
			}
			Assert.IsNotNull(field, $"Field '{fieldName}' not found in {instance.GetType()} or its base types.");
			return (T)field.GetValue(instance);
		}


		// Tests for GetFilteredSortedListInBackground
		[Test]
		public void GetFilteredSortedListInBackground_WithSorterAndFilter_ReturnsFilteredAndSorted()
		{
			var owningObjForTest = new MockCmObject { Hvo = 2000 };
			SetField(m_list, "m_owningObject", owningObjForTest);
			SetField(m_list, "m_flid", 2); // Use a distinct flid

			var mockSda = new Mock<ISilDataAccessManaged>();
			var hvosInSource = new List<int> { 10, 5, 20, 15 }; // Unsorted
			mockSda.Setup(x => x.GetVectorProperty(owningObjForTest.Hvo, 2, true)).Returns(hvosInSource);
			var mdcMock = new Mock<IFwMetaDataCache>();
			mockSda.Setup(x => x.MetaDataCache).Returns(mdcMock.Object);


			var originalPublisher = GetField<ISilDataAccessManaged>(m_list, "m_publisher");
			SetField(m_list, "m_publisher", mockSda.Object);

			var mockSorter = new MockRecordSorter(); // Sorts by HVO ascending
			SetRecordListSorter(m_list, mockSorter);

			var mockFilter = new Mock<RecordFilter>();
			mockFilter.Setup(f => f.Accept(It.IsAny<IManyOnePathSortItem>()))
				.Returns<IManyOnePathSortItem>(item => item.RootObjectHvo >= 10 && item.RootObjectHvo < 20); // Accepts 10, 15

			SetRecordListFilter(m_list, mockFilter.Object);

			ArrayList result = m_list.GetFilteredSortedListInBackground();

			Assert.IsTrue(mockSorter.SortCalled, "Sorter.Sort should have been called.");
			Assert.AreEqual(2, result.Count, "Should contain 2 items after filtering.");
			Assert.AreEqual(10, ((IManyOnePathSortItem)result[0]).RootObjectHvo, "First item should be HVO 10 (sorted).");
			Assert.AreEqual(15, ((IManyOnePathSortItem)result[1]).RootObjectHvo, "Second item should be HVO 15 (sorted).");

			SetField(m_list, "m_publisher", originalPublisher); // Restore
			SetRecordListFilter(m_list, null);
			SetRecordListSorter(m_list, null);
		}

		[Test]
		public void GetFilteredSortedListInBackground_WithFilterNoSorter_ReturnsFilteredUnsorted()
		{
			var owningObjForTest = new MockCmObject { Hvo = 2001 };
			SetField(m_list, "m_owningObject", owningObjForTest);
			SetField(m_list, "m_flid", 3);

			var mockSda = new Mock<ISilDataAccessManaged>();
			var hvosInSource = new List<int> { 10, 5, 20, 15 }; // Original order
			mockSda.Setup(x => x.GetVectorProperty(owningObjForTest.Hvo, 3, true)).Returns(hvosInSource);
			var mdcMock = new Mock<IFwMetaDataCache>();
			mockSda.Setup(x => x.MetaDataCache).Returns(mdcMock.Object);

			var originalPublisher = GetField<ISilDataAccessManaged>(m_list, "m_publisher");
			SetField(m_list, "m_publisher", mockSda.Object);

			SetRecordListSorter(m_list, null); // No sorter

			var mockFilter = new Mock<RecordFilter>();
			mockFilter.Setup(f => f.Accept(It.IsAny<IManyOnePathSortItem>()))
				.Returns<IManyOnePathSortItem>(item => item.RootObjectHvo == 5 || item.RootObjectHvo == 15); // Accepts 5, 15

			SetRecordListFilter(m_list, mockFilter.Object);

			ArrayList result = m_list.GetFilteredSortedListInBackground();

			Assert.AreEqual(2, result.Count);
			Assert.AreEqual(5, ((IManyOnePathSortItem)result[0]).RootObjectHvo, "First item should be HVO 5 (original order of accepted items).");
			Assert.AreEqual(15, ((IManyOnePathSortItem)result[1]).RootObjectHvo, "Second item should be HVO 15.");

			SetField(m_list, "m_publisher", originalPublisher);
			SetRecordListFilter(m_list, null);
		}


		// Tests for CompleteReloadListFromBackground

		[SetUp]
		public void CompleteReloadTestSetup() // Separate SetUp for these tests if state needs to be cleaner
		{
			_listChangedEventFired = false;
			_listChangedEventArgs = null;
			m_list.ListChanged += HandleListChanged;
			// Ensure a clean list state for each CompleteReload... test
			SetSortedObjects(m_list, new ArrayList());
			SetField(m_list, "m_currentIndex", -1);
			SetField(m_list, "m_hvoCurrent", 0);
			SetField(m_list, "m_reloadingListInternal", false); // Assuming internal field for reload state
		}

		[TearDown]
		public void CompleteReloadTestTearDown()
		{
			m_list.ListChanged -= HandleListChanged;
		}


		[Test]
		public void CompleteReloadListFromBackground_UpdatesSortedObjectsAndCurrentIndex_NoPreviousSelection()
		{
			var newItems = new ArrayList { new MockManyOnePathSortItem(100), new MockManyOnePathSortItem(101) };

			m_list.CompleteReloadListFromBackground(newItems, 0);

			Assert.AreSame(newItems, GetSortedObjects(m_list), "SortedObjects should be the new list.");
			Assert.AreEqual(0, m_list.CurrentIndex, "CurrentIndex should be 0 for non-empty list.");
			Assert.AreEqual(100, GetField<int>(m_list, "m_hvoCurrent"), "m_hvoCurrent should be updated.");
		}

		[Test]
		public void CompleteReloadListFromBackground_UpdatesAndPreservesSelectionHvo()
		{
			SetSortedObjects(m_list, new ArrayList { new MockManyOnePathSortItem(10), new MockManyOnePathSortItem(20) });
			SetField(m_list, "m_currentIndex", 1); // Current is HVO 20
			SetField(m_list, "m_hvoCurrent", 20);

			var newItems = new ArrayList { new MockManyOnePathSortItem(5), new MockManyOnePathSortItem(20), new MockManyOnePathSortItem(30) };
			// HVO 20 is now at index 1 (same index, but list changed)

			m_list.CompleteReloadListFromBackground(newItems, 20); // hvoCurrentBeforeReload was 20

			Assert.AreSame(newItems, GetSortedObjects(m_list));
			Assert.AreEqual(1, m_list.CurrentIndex, "CurrentIndex should point to HVO 20's new position.");
			Assert.AreEqual(20, GetField<int>(m_list, "m_hvoCurrent"));
		}

		[Test]
		public void CompleteReloadListFromBackground_SetsIndexToZeroIfOldHvoNotFound()
		{
			SetSortedObjects(m_list, new ArrayList { new MockManyOnePathSortItem(10) });
			SetField(m_list, "m_currentIndex", 0);
			SetField(m_list, "m_hvoCurrent", 10);

			var newItems = new ArrayList { new MockManyOnePathSortItem(100), new MockManyOnePathSortItem(101) };
			// HVO 10 is no longer in the list.

			m_list.CompleteReloadListFromBackground(newItems, 10); // hvoCurrentBeforeReload was 10

			Assert.AreSame(newItems, GetSortedObjects(m_list));
			Assert.AreEqual(0, m_list.CurrentIndex, "CurrentIndex should be 0 as old HVO not found.");
			Assert.AreEqual(100, GetField<int>(m_list, "m_hvoCurrent"));
		}

		[Test]
		public void CompleteReloadListFromBackground_FiresListChangedEvent()
		{
			var newItems = new ArrayList { new MockManyOnePathSortItem(100) };
			m_list.CompleteReloadListFromBackground(newItems, 0);

			Assert.IsTrue(_listChangedEventFired, "ListChanged event should have fired.");
			Assert.IsNotNull(_listChangedEventArgs, "ListChangedEventArgs should not be null.");
			// Could check e.g. _listChangedEventArgs.Actions depending on logic for SuppressSaveOnChangeRecord
		}

		[Test]
		public void CompleteReloadListFromBackground_SetsReloadingListFlagToFalse()
		{
			// Assuming m_reloadingListInternal is the field corresponding to m_reloadingList
			// and it was made internal for RecordClerk to set.
			// Here, we test that CompleteReloadListFromBackground sets it to false.
			SetField(m_list, "m_reloadingListInternal", true); // Simulate it was true before call

			m_list.CompleteReloadListFromBackground(new ArrayList(), 0);

			Assert.IsFalse(GetField<bool>(m_list, "m_reloadingListInternal"), "m_reloadingListInternal should be false after completion.");
		}
	}
}
