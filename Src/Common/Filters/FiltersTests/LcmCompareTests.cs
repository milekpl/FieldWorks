using NUnit.Framework;
using SIL.FieldWorks.Filters;
using SIL.LCModel; // For ICmObject and LcmCache (though LcmCache might be null or mocked)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SIL.LCModel.Core.KernelInterfaces; // For ICmObject
using SIL.LCModel.DomainServices; // For ICmObject (if different part) - check actual ICmObject namespace

// Minimal ICmObject implementation for testing GetProperty
namespace SIL.FieldWorks.Filters.Tests
{
    public class MockCmObject : ICmObject
    {
        public string StringProp { get; set; }
        public int IntProp { get; set; }
        public bool BoolProp { get; set; }
        private Guid m_guid = Guid.NewGuid();

        // Implement minimal ICmObject members. Most won't be used by GetProperty.
        public int Hvo { get; set; }
        public int ClassID { get; private set; }
        public bool IsDirty { get { return false; } }
        public bool IsNew { get { return false; } }
        public ICmObject Owner { get { return null; } }
        public int OwningFlid { get { return 0; } }
        public int OwnOrd { get { return 0; } }
        public Guid Guid { get { return m_guid; } }
        public ISilDataAccess SilDataAccess { get { return null; } }
        public LcmCache Cache { get { return null; } } // LcmCache might be needed by LcmCompare constructor
        public void SetDirty() { }
        public void SetNew(bool isNew) { }
        public string ShortName { get { return "MockCmObject"; } }
		public string SortKey { get; set; }
		public int SortKeyWs { get { return 0; } }
		public ICmObject RootObject { get { return this; } }
		public IServiceProvider ServiceProvider { get { return null; } }
		public void CheckDisposed() { }
		public bool IsDisposed { get { return false; } }
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		public void Dispose() { }
		public string GetPropertyXml(string propertyName, bool convertGuidsToLocalIds) { return string.Empty; }
		public string GetMultiStringAlt(int flid, int ws, bool fallBackToAnalysisDefault) { return string.Empty; }
		public void SetMultiStringAlt(int flid, int ws, string value) { }
		public string BestAnalysisVernacularAlternative(int flid) { return string.Empty; }
		public string BestAnalysisAlternative(int flid) { return string.Empty; }
		public string BestVernacularAlternative(int flid) { return string.Empty; }
		public bool TryGetObject(int hvo, out ICmObject obj) { obj = null; return false; }

		// Added to satisfy IGetGuid implementation (if ICmObject inherits it)
		public Guid GetGuid() { return m_guid; }
		public void SetGuid(Guid guid) { m_guid = guid; }

		// Potentially needed based on ICmObject definition
		public int GetActualPropertyType(int flid) { return 0;}
		public System.Collections.Generic.IEnumerable<int> GetVectorProperty(int flid, bool includeGhosts) { return new List<int>(); }
		public int GetVectorSize(int flid) { return 0; }
		public bool IsValidObject() { return true; }
		public bool IsValidOrphan() { return true; }
		public string GetShortName(int wsUi) { return ShortName; }
		public void Dump(System.IO.TextWriter writer, DumpStyle style, HashSet<int> objectsToDump) { }
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
		public void SetTextProperty(int flid, IStText value) { }
		public void SetUnicodeProperty(int flid, string value) { }
		public string GetUnicodeProperty(int flid) { return string.Empty; }
		public IStText GetTextProperty(int flid) { return null; }
		public int GetObjProperty(int flid) { return 0; }
		public int GetIntProperty(int flid) { return 0; }
		public bool GetBoolProperty(int flid) { return false; }
		public void SetBoolProperty(int flid, bool val) { }
		public System.DateTime GetDateProperty(int flid) { return DateTime.MinValue; }
		public void SetDateProperty(int flid, System.DateTime val) { }
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
		public void GetPossibilities(int flid, Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
		public bool IsValidValue(int flid, int hvo) { return false; }
		public bool IsValidValue(int flid, string value, int ws) { return false; }
		public bool IsValidValue(int flid, string value) { return false; }
		public bool CanSetProperty(int flid) { return false; }
		public string GetPropertyLabel(int flid, int wsUi) { return string.Empty; }
		public void GetIndirectPossibilities(int flid, Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
		public void GetDirectPossibilities(int flid, Set<ICmPossibility> possibilities, bool fIncludeHidden) { }
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
		public bool CanReorderField(int flid) { return false; }
		public bool GetFieldVisibility(int flid) { return false; }
		public string GetFieldHelpFile(int flid) { return string.Empty; }
		public string GetFieldHelpTopic(int flid) { return string.Empty; }
		public bool IsSameOrSubclassOf(int clsid, int clsidOther) { return false; }
		public bool IsSameOrSubclassOf(string className, string classNameOther) { return false; }
		public string GetClassLabel(int clsid, int wsUi) { return string.Empty; }
		public string GetClassDescription(int clsid, int wsUi) { return string.Empty; }
		public bool GetClassAbstract(int clsid) { return false; }
		public bool GetClassUserCanCreate(int clsid) { return false; }
		public bool GetClassUserCanDelete(int clsid) { return false; }
		public int GetBaseClsId(int clsid) { return 0; }
		public int[] GetDirectSubclasses(int clsid) { return null; }
		public int[] GetAllSubclasses(int clsid) { return null; }
		public bool IsValidClass(int clsid) { return false; }
		public bool IsValidField(int flid) { return false; }
		public bool IsValidFlid(int flid) { return false; }
		public bool IsValidHvo(int hvo) { return false; }
		public bool FieldIsAtomic(int flid) { return false; }
		public bool FieldIsCollection(int flid) { return false; }
		public bool FieldIsObject(int flid) { return false; }
		public bool FieldIsVector(int flid) { return false; }
		public bool FieldIsProtected(int flid) { return false; }
		public bool FieldIsReadOnly(int flid) { return false; }
		public bool FieldIsMultiString(int flid) { return false; }
		public bool FieldIsBigString(int flid) { return false; }
		public bool FieldIsBigText(int flid) { return false; }
		public bool FieldIsText(int flid) { return false; }
		public bool FieldIsUnicode(int flid) { return false; }
		public bool FieldIsInteger(int flid) { return false; }
		public bool FieldIsGuid(int flid) { return false; }
		public bool FieldIsDate(int flid) { return false; }
		public bool FieldIsBinary(int flid) { return false; }
		public bool FieldIsBoolean(int flid) { return false; }
		public bool FieldIsGenDate(int flid) { return false; }
		public int FieldGetPropClass(int flid) { return 0; }
		public bool FieldIsCustom(int flid) { return false; }
		public int FieldGetType(int flid) { return 0; }
		public string FieldGetNameForCustomProperty(int flid) { return string.Empty; }
		public int FieldGetWsForCustomProperty(int flid) { return 0; }
		public bool FieldGetIsWsSpecificForCustomProperty(int flid) { return false; }
		public string FieldGetListRootGuidForCustomProperty(int flid) { return string.Empty; }
		public bool FieldGetCanDeleteCustomProperty(int flid) { return false; }
		public bool FieldGetIsCustomPropWritable(int flid) { return false; }
		public bool FieldIsCustomFieldUsed(int flid) { return false; }
		public bool FieldIsCustomFieldReadOnly(int flid) { return false; }
		public bool FieldIsCustomFieldProtected(int flid) { return false; }
		public string FieldGetCustomPropLabel(int flid, int ws) { return string.Empty; }
		public string FieldGetCustomPropDescription(int flid, int ws) { return string.Empty; }
		public string FieldGetCustomPropHelp(int flid, int ws) { return string.Empty; }
		public string FieldGetCustomPropGenericName(int flid, int ws) { return string.Empty; }
		public string FieldGetCustomPropMin(int flid) { return string.Empty; }
		public string FieldGetCustomPropMax(int flid) { return string.Empty; }
		public int FieldGetCustomPropStringMax(int flid) { return 0; }
		public string FieldGetCustomPropBigStringDef(int flid) { return string.Empty; }
		public string FieldGetCustomPropBigStringUserDef(int flid) { return string.Empty; }
		public bool FieldGetCustomPropBigStringIsMulti(int flid) { return false; }
		public string FieldGetCustomPropBigStringValidation(int flid) { return string.Empty; }
		public string FieldGetCustomPropUserPrompt(int flid) { return string.Empty; }
		public bool FieldGetCustomPropIsSecure(int flid) { return false; }
		public string FieldGetCustomPropRegExp(int flid) { return string.Empty; }
		public string[] FieldGetCustomPropChoices(int flid) { return null; }
		public bool FieldIsValidValueForCustomList(int flid, string value) { return false; }
		public int[] FieldGetCustomFields(int clsid, bool fIncludeInherited, int fieldTypeFilter) { return null; }
		public bool FieldCanReorderField(int flid) { return false; }
		public bool FieldGetCustomPropVisibility(int flid) { return false; }
		public string FieldGetCustomPropHelpFile(int flid) { return string.Empty; }
		public string FieldGetCustomPropHelpTopic(int flid) { return string.Empty; }
		public string FieldGetOwnClsName(int flid) { return string.Empty; }
		public int FieldGetOwnClsId(int flid) { return 0; }
    }

    [TestFixture]
    public class LcmCompareTests
    {
        private MethodInfo _getLcmComparePropertyMethod;
        private FieldInfo _propertyInfoCacheField;
        private Type _lcmCompareType;

        [SetUp]
        public void SetUp()
        {
            // RecordSorter is public, LcmCompare is protected nested.
            // Need to get LcmCompare type via reflection.
            _lcmCompareType = typeof(RecordSorter).GetNestedType("LcmCompare", BindingFlags.NonPublic | BindingFlags.Public);
            Assert.IsNotNull(_lcmCompareType, "Could not find RecordSorter.LcmCompare type.");

            _getLcmComparePropertyMethod = _lcmCompareType.GetMethod("GetProperty", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public); // It's protected
            Assert.IsNotNull(_getLcmComparePropertyMethod, "Could not find GetProperty method in LcmCompare.");

            _propertyInfoCacheField = _lcmCompareType.GetField("PropertyInfoCache", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(_propertyInfoCacheField, "Could not find PropertyInfoCache field in LcmCompare.");

            // Clear the cache before each test
            ClearPropertyInfoCache();
        }

        private void ClearPropertyInfoCache()
        {
            var cache = _propertyInfoCacheField.GetValue(null) as IDictionary;
            if (cache != null)
            {
                cache.Clear();
            }
        }

        private object InvokeGetProperty(object lcmCompareInstance, ICmObject target, string propertyName)
        {
            return _getLcmComparePropertyMethod.Invoke(lcmCompareInstance, new object[] { target, propertyName });
        }

        private IDictionary GetPropertyInfoCache()
        {
            return _propertyInfoCacheField.GetValue(null) as IDictionary;
        }

        // Constructor for LcmCompare: public LcmCompare(string propertyName, LcmCache cache)
        // We'll pass null for LcmCache as GetProperty itself doesn't use it directly for reflection.
        private object CreateLcmCompareInstance(string propertyName, LcmCache cache = null)
        {
            // LcmCompare constructor is public within RecordSorter but LcmCompare is protected.
            // Activator.CreateInstance can be used if a public constructor exists.
            // If LcmCompare's constructor were internal or protected, we'd need more complex reflection.
            // Assuming LcmCache can be null for these tests.
            return Activator.CreateInstance(_lcmCompareType, propertyName, cache);
        }

        [Test]
        public void GetProperty_FirstCall_PopulatesCache()
        {
            var lcmComparer = CreateLcmCompareInstance("StringProp");
            var mockObj = new MockCmObject { StringProp = "Test" };

            InvokeGetProperty(lcmComparer, mockObj, "StringProp");

            var cache = GetPropertyInfoCache();
            var key = Tuple.Create(typeof(MockCmObject), "StringProp");
            Assert.IsTrue(cache.Contains(key), "Cache should contain the property info after first call.");
            Assert.IsNotNull(cache[key], "Cached PropertyInfo should not be null.");
        }

        [Test]
        public void GetProperty_SecondCall_UsesCache()
        {
            var lcmComparer = CreateLcmCompareInstance("StringProp");
            var mockObj1 = new MockCmObject { StringProp = "Test1" };
            var mockObj2 = new MockCmObject { StringProp = "Test2" };

            // First call - populates cache
            InvokeGetProperty(lcmComparer, mockObj1, "StringProp");
            var cache = GetPropertyInfoCache();
            var key = Tuple.Create(typeof(MockCmObject), "StringProp");
            var cachedInfo1 = cache[key];

            // Second call - should use cache
            InvokeGetProperty(lcmComparer, mockObj2, "StringProp");
            var cachedInfo2 = cache[key]; // Get it again to ensure it's the same instance

            Assert.AreSame(cachedInfo1, cachedInfo2, "PropertyInfo instance from cache should be the same.");
            Assert.AreEqual(1, cache.Count, "Cache should still only have one entry for this property.");
        }

        [Test]
        public void GetProperty_ReturnsCorrectStringValue()
        {
            var lcmComparer = CreateLcmCompareInstance("StringProp");
            var mockObj = new MockCmObject { StringProp = "Hello World" };

            object value = InvokeGetProperty(lcmComparer, mockObj, "StringProp");
            Assert.AreEqual("Hello World", value);
        }

        [Test]
        public void GetProperty_ReturnsCorrectIntValue()
        {
            var lcmComparer = CreateLcmCompareInstance("IntProp");
            var mockObj = new MockCmObject { IntProp = 123 };

            object value = InvokeGetProperty(lcmComparer, mockObj, "IntProp");
            Assert.AreEqual(123, value);
        }

        [Test]
        public void GetProperty_ReturnsCorrectBoolValue()
        {
            var lcmComparer = CreateLcmCompareInstance("BoolProp");
            var mockObj = new MockCmObject { BoolProp = true };

            object value = InvokeGetProperty(lcmComparer, mockObj, "BoolProp");
            Assert.AreEqual(true, value);
        }

        [Test]
        public void GetProperty_MultipleProperties_CachedCorrectly()
        {
            var lcmComparerString = CreateLcmCompareInstance("StringProp"); // Comparer instance doesn't matter for GetProperty's caching
            var lcmComparerInt = CreateLcmCompareInstance("IntProp");
            var mockObj = new MockCmObject { StringProp = "MultiTest", IntProp = 456 };

            InvokeGetProperty(lcmComparerString, mockObj, "StringProp");
            InvokeGetProperty(lcmComparerInt, mockObj, "IntProp");

            var cache = GetPropertyInfoCache();
            var keyString = Tuple.Create(typeof(MockCmObject), "StringProp");
            var keyInt = Tuple.Create(typeof(MockCmObject), "IntProp");

            Assert.IsTrue(cache.Contains(keyString), "Cache should contain StringProp.");
            Assert.IsTrue(cache.Contains(keyInt), "Cache should contain IntProp.");
            Assert.AreEqual(2, cache.Count, "Cache should have two entries.");
        }

        public class AnotherMockCmObject : ICmObject
        {
            public string AnotherStringProp { get; set; }
            // Implement minimal ICmObject members as in MockCmObject
            public int Hvo { get; set; } public int ClassID { get; private set; } public bool IsDirty { get { return false; } } public bool IsNew { get { return false; } } public ICmObject Owner { get { return null; } } public int OwningFlid { get { return 0; } } public int OwnOrd { get { return 0; } } public Guid Guid { get { return Guid.NewGuid(); } } public ISilDataAccess SilDataAccess { get { return null; } } public LcmCache Cache { get { return null; } } public void SetDirty() { } public void SetNew(bool isNew) { } public string ShortName { get { return "AnotherMockCmObject"; } } public string SortKey { get; set; } public int SortKeyWs { get { return 0; } } public ICmObject RootObject { get { return this; } } public IServiceProvider ServiceProvider { get { return null; } } public void CheckDisposed() { } public bool IsDisposed { get { return false; } } public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged; public void Dispose() { } public string GetPropertyXml(string propertyName, bool convertGuidsToLocalIds) { return string.Empty; } public string GetMultiStringAlt(int flid, int ws, bool fallBackToAnalysisDefault) { return string.Empty; } public void SetMultiStringAlt(int flid, int ws, string value) { } public string BestAnalysisVernacularAlternative(int flid) { return string.Empty; } public string BestAnalysisAlternative(int flid) { return string.Empty; } public string BestVernacularAlternative(int flid) { return string.Empty; } public bool TryGetObject(int hvo, out ICmObject obj) { obj = null; return false; } public Guid GetGuid() { return Guid.NewGuid(); } public void SetGuid(Guid guid) { }
			public int GetActualPropertyType(int flid) { return 0;} public System.Collections.Generic.IEnumerable<int> GetVectorProperty(int flid, bool includeGhosts) { return new List<int>(); } public int GetVectorSize(int flid) { return 0; } public bool IsValidObject() { return true; } public bool IsValidOrphan() { return true; } public string GetShortName(int wsUi) { return ShortName; } public void Dump(System.IO.TextWriter writer, DumpStyle style, HashSet<int> objectsToDump) { } public int GetOwningDepth() { return 0; } public ICmObject GetOrSetOwner(int hvoNewOwner, int flid, int ord) { return null; } public void SetOwner(ICmObject newOwner, int flid, int ord) { } public void SetOwnOrd(int owningFlid, int newOrd) { } public ICmObject Clone() { return null; } public void SetIntProperty(int flid, int value) { } public void SetStringProperty(int flid, string value, int ws) { } public void DeleteObjProperty(int flid) { } public void DeleteObjProperty(int flid, int hvoObj) { } public void InsertObjVectorProperty(int flid, int hvoObj, int ord) { } public void SetObjProperty(int flid, int hvo) { } public void SetTextProperty(int flid, IStText value) { } public void SetUnicodeProperty(int flid, string value) { } public string GetUnicodeProperty(int flid) { return string.Empty; } public IStText GetTextProperty(int flid) { return null; } public int GetObjProperty(int flid) { return 0; } public int GetIntProperty(int flid) { return 0; } public bool GetBoolProperty(int flid) { return false; } public void SetBoolProperty(int flid, bool val) { } public System.DateTime GetDateProperty(int flid) { return DateTime.MinValue; } public void SetDateProperty(int flid, System.DateTime val) { } public byte[] GetBinaryProperty(int flid) { return null; } public void SetBinaryProperty(int flid, byte[] val) { } public bool IsNull(int flid) { return false; } public bool IsValidObjectId(int hvo) { return false; } public string GetStringProperty(int flid, int ws) { return string.Empty; } public int GetStringPropVernWs(int flid) { return 0; } public int GetStringPropAnalWs(int flid) { return 0; } public void SetDummyReferences(ICmObject owner) { } public string GetName(int ws) { return string.Empty; } public string FullName { get { return string.Empty; } } public string GetPropertyLabel(int flid) { return string.Empty; } public bool IsValidProperty(int flid, bool fStrict) { return false; } public bool IsAtomicProp(int flid) { return false; } public bool IsCollectionProp(int flid) { return false; } public bool IsObjectProp(int flid) { return false; } public bool IsVectorProp(int flid) { return false; } public bool IsProtected(int flid) { return false; } public bool IsReadOnly(int flid) { return false; } public bool IsMultiStringProp(int flid) { return false; } public bool IsBigStringProp(int flid) { return false; } public bool IsBigTextProp(int flid) { return false; } public bool IsTextProp(int flid) { return false; } public bool IsUnicodeProp(int flid) { return false; } public bool IsIntegerProp(int flid) { return false; } public bool IsGuidProp(int flid) { return false; } public bool IsDateProp(int flid) { return false; } public bool IsBinaryProp(int flid) { return false; } public bool IsBooleanProp(int flid) { return false; } public bool IsGenDateProp(int flid) { return false; } public int GetPropClass(int flid) { return 0; } public bool IsCustomProperty(int flid) { return false; } public void MakeNewCustomField(string fieldName, int type, int ws, bool isWsSpecific) { } public void DeleteCustomField(int flid) { } public void MakeNewCustomField(string fieldName, int type, int ws, bool isWsSpecific, string listRootGuid) { } public int GetFieldType(int flid) { return 0; } public string GetNameForCustomProperty(int flid) { return string.Empty; } public int GetWsForCustomProperty(int flid) { return 0; } public bool GetIsWsSpecificForCustomProperty(int flid) { return false; } public string GetListRootGuidForCustomProperty(int flid) { return string.Empty; } public bool GetCanDeleteCustomProperty(int flid) { return false; } public bool GetIsCustomPropWritable(int flid) { return false; } public bool IsCustomFieldUsed(int flid) { return false; } public bool IsCustomFieldReadOnly(int flid) { return false; } public bool IsCustomFieldProtected(int flid) { return false; } public void SetCustomPropLabel(int flid, string label, int ws) { } public string GetCustomPropLabel(int flid, int ws) { return string.Empty; } public void SetCustomPropDescription(int flid, string description, int ws) { } public string GetCustomPropDescription(int flid, int ws) { return string.Empty; } public void SetCustomPropHelp(int flid, string helpString, int ws) { } public string GetCustomPropHelp(int flid, int ws) { return string.Empty; } public void SetCustomPropGenericName(int flid, string genericName, int ws) { } public string GetCustomPropGenericName(int flid, int ws) { return string.Empty; } public void SetCustomPropMin(int flid, string min) { } public string GetCustomPropMin(int flid) { return string.Empty; } public void SetCustomPropMax(int flid, string max) { } public string GetCustomPropMax(int flid) { return string.Empty; } public void SetCustomPropStringMax(int flid, int max) { } public int GetCustomPropStringMax(int flid) { return 0; } public void SetCustomPropBigStringDef(int flid, string def) { } public string GetCustomPropBigStringDef(int flid) { return string.Empty; } public void SetCustomPropBigStringUserDef(int flid, string userDef) { } public string GetCustomPropBigStringUserDef(int flid) { return string.Empty; } public bool GetCustomPropBigStringIsMulti(int flid) { return false; } public string GetCustomPropBigStringValidation(int flid) { return string.Empty; } public void SetCustomPropBigStringValidation(int flid, string validation) { } public string GetCustomPropUserPrompt(int flid) { return string.Empty; } public void SetCustomPropUserPrompt(int flid, string prompt) { } public bool GetCustomPropIsSecure(int flid) { return false; } public void SetCustomPropIsSecure(int flid, bool isSecure) { } public bool IsSecureCustomProperty(int flid) { return false; } public string GetCustomPropRegExp(int flid) { return string.Empty; } public void SetCustomPropRegExp(int flid, string regExp) { } public string[] GetCustomPropChoices(int flid) { return null; } public void SetCustomPropChoices(int flid, string[] choices) { } public bool IsValidValueForCustomList(int flid, string value) { return false; } public int[] GetCustomFields(int clsid, bool fIncludeInherited, int fieldTypeFilter) { return null; } public void ReorderCustomField(int flid, int flidBefore) { } public void SetCustomPropVisibility(int flid, bool isVisible) { } public bool GetCustomPropVisibility(int flid) { return false; } public string GetCustomPropHelpFile(int flid) { return string.Empty; } public void SetCustomPropHelpFile(int flid, string helpFile) { } public void SetCustomPropHelpTopic(int flid, string helpTopic) { } public string GetCustomPropHelpTopic(int flid) { return string.Empty; } public bool IsSameOrSubclassOf(int clsidOther) { return false; } public bool IsContainedIn(int hvo) { return false; } public ICmObject GetAncestor(int clsid) { return null; } public ICmObject GetAncestor(int[] clsid) { return null; } public ICmObject GetOuterCmObject() { return null; } public void GetPossibilities(int flid, Set<ICmObject> possibilities, bool fIncludeHidden) { } public bool IsValidValue(int flid, int hvo) { return false; } public bool IsValidValue(int flid, string value, int ws) { return false; } public bool IsValidValue(int flid, string value) { return false; } public bool CanSetProperty(int flid) { return false; } public string GetPropertyLabel(int flid, int wsUi) { return string.Empty; } public void GetIndirectPossibilities(int flid, Set<ICmObject> possibilities, bool fIncludeHidden) { } public void GetDirectPossibilities(int flid, Set<ICmObject> possibilities, bool fIncludeHidden) { } public bool IsValidObject(int flid, int hvo) { return false; } public bool IsValidObject(int flid, int hvo, bool fStrict) { return false; } public string GetNameForWs(int ws) { return string.Empty; } public int GetDefaultAnalWs() { return 0; } public int GetDefaultUserWs() { return 0; } public int GetDefaultVernWs() { return 0; } public bool RemoveSdaNotification(IVwNotifyChange nc) { return false; } public void AddSdaNotification(IVwNotifyChange nc) { } public string GetPathToCmObject(int hvoTarget) { return string.Empty; } public bool IsStringPropEmpty(int flid) { return false; } public bool IsComplexForm(int flid) { return false; } public bool IsEncrypted(int flid) { return false; } public string GetPropertyType(int flid) { return string.Empty; } public string GetPropertyClassName(int flid) { return string.Empty; } public string GetPropertySig(int flid) { return string.Empty; } public int GetFieldId(string fieldName, bool fMustExist) { return 0; } public string GetFieldName(int flid) { return string.Empty; } public bool IsOwnedBy(int hvoPossibleOwner) { return false; } public bool FieldExists(int flid) { return false; } public string PropNameFromFlid(int flid) { return string.Empty; } public bool CanCreateCustomFields(int clsid) { return false; } public int GetNamedCustomFieldId(int clsid, string fieldName, int type, int ws, bool isWsSpecific) { return 0; } public void SetCustomPropVisibility(int flid, bool isVisible, bool isOverride) { } public void SetCustomPropHelpFile(int flid, string helpFile, bool isOverride) { } public void SetCustomPropHelpTopic(int flid, string helpTopic, bool isOverride) { } public void SetCustomPropUserPrompt(int flid, string prompt, bool isOverride) { } public int GetFieldWs(int flid) { return 0; } public bool IsFieldWsSpecific(int flid) { return false; } public string GetListRootGuid(int flid) { return string.Empty; } public bool TryGetGuidProperty(int flid, out Guid guid) { guid = Guid.Empty; return false; } public void SetGuidProperty(int flid, Guid guid) { } public bool IsGuidEmpty(int flid) { return false; } public System.Collections.Generic.IEnumerable<int> GetMultiIntProperty(int flid) { return null; } public void SetMultiIntProperty(int flid, System.Collections.Generic.IEnumerable<int> values) { } public void DeleteMultiIntProperty(int flid, int value) { } public bool IsMultiIntPropEmpty(int flid) { return false; } public int GetMultiIntPropCount(int flid) { return 0; } public string GetXmlPathToCmObject(int hvoTarget) { return string.Empty; } public int GetClassId(string className) { return 0; } public string GetClassName(int clsid) { return string.Empty; } public string GetFullClassName(int clsid) { return string.Empty; } public string GetFieldSig(int flid) { return string.Empty; } public int GetFieldClsid(int flid) { return 0; } public int GetFieldDstClsid(int flid) { return 0; } public int GetFieldAtomicOrCustomType(int flid) { return 0; } public bool IsFieldSystemInternal(int flid) { return false; } public bool IsFieldUserVisible(int flid) { return false; } public string GetFieldLabel(int flid, int wsUi) { return string.Empty; } public string GetFieldDescription(int flid, int wsUi) { return string.Empty; } public string GetFieldHelpString(int flid, int wsUi) { return string.Empty; } public string GetFieldGenericName(int flid, int wsUi) { return string.Empty; } public string GetFieldMin(int flid) { return string.Empty; } public string GetFieldMax(int flid) { return string.Empty; } public int GetFieldStringMax(int flid) { return 0; } public string GetFieldBigStringDef(int flid) { return string.Empty; } public string GetFieldBigStringUserDef(int flid) { return string.Empty; } public bool GetFieldBigStringIsMulti(int flid) { return false; } public string GetFieldBigStringValidation(int flid) { return string.Empty; } public string GetFieldUserPrompt(int flid) { return string.Empty; } public bool GetFieldIsSecure(int flid) { return false; } public string GetFieldRegExp(int flid) { return string.Empty; } public string[] GetFieldChoices(int flid) { return null; } public bool IsValidValueForFieldList(int flid, string value) { return false; } public int[] GetFields(int clsid, bool fIncludeInherited, int fieldTypeFilter) { return null; } public bool CanReorderField(int flid) { return false; } public bool GetFieldVisibility(int flid) { return false; } public string GetFieldHelpFile(int flid) { return string.Empty; } public string GetFieldHelpTopic(int flid) { return string.Empty; } public string FieldGetOwnClsName(int flid) { return string.Empty; } public int FieldGetOwnClsId(int flid) { return 0; }

        }

        [Test]
        public void GetProperty_DifferentTypes_CachedCorrectly()
        {
            var lcmComparer = CreateLcmCompareInstance("DummyProp"); // Property name for comparer doesn't affect GetProperty directly
            var mockObj1 = new MockCmObject { StringProp = "Type1Test" };
            var mockObj2 = new AnotherMockCmObject { AnotherStringProp = "Type2Test" };

            InvokeGetProperty(lcmComparer, mockObj1, "StringProp");
            InvokeGetProperty(lcmComparer, mockObj2, "AnotherStringProp");

            var cache = GetPropertyInfoCache();
            var key1 = Tuple.Create(typeof(MockCmObject), "StringProp");
            var key2 = Tuple.Create(typeof(AnotherMockCmObject), "AnotherStringProp");

            Assert.IsTrue(cache.Contains(key1), "Cache should contain MockCmObject.StringProp.");
            Assert.IsTrue(cache.Contains(key2), "Cache should contain AnotherMockCmObject.AnotherStringProp.");
            Assert.AreEqual(2, cache.Count, "Cache should have two entries for different types/properties.");
        }

        [Test]
        public void GetProperty_InvalidPropertyName_ThrowsArgumentException()
        {
            var lcmComparer = CreateLcmCompareInstance("InvalidProp");
            var mockObj = new MockCmObject();

            Assert.Throws<TargetInvocationException>(() => InvokeGetProperty(lcmComparer, mockObj, "InvalidProp"),
                "Should throw TargetInvocationException (wrapping ArgumentException) for invalid property.");

            // Optionally, check inner exception
            try
            {
                InvokeGetProperty(lcmComparer, mockObj, "InvalidProp");
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsInstanceOf<ArgumentException>(ex.InnerException, "Inner exception should be ArgumentException.");
            }
        }

        // Indirect test for cache affecting sorting (PropertyRecordSorter uses LcmCompare)
        [Test]
        public void PropertyRecordSorter_WithLcmCompare_SortsCorrectlyMultipleTimes()
        {
            var list = new ArrayList
            {
                new ManyOnePathSortItem(new MockCmObject { StringProp = "Charlie", Hvo=1 }, null, null),
                new ManyOnePathSortItem(new MockCmObject { StringProp = "Alpha", Hvo=2 }, null, null),
                new ManyOnePathSortItem(new MockCmObject { StringProp = "Bravo", Hvo=3 }, null, null)
            };

            var sorter = new PropertyRecordSorter("StringProp");
            // PropertyRecordSorter will create its own LcmCompare.
            // We rely on it using the caching version.

            // First sort
            sorter.Sort(list);
            Assert.AreEqual("Alpha", ((MockCmObject)((IManyOnePathSortItem)list[0]).KeyObjectUsing(null)).StringProp);
            Assert.AreEqual("Bravo", ((MockCmObject)((IManyOnePathSortItem)list[1]).KeyObjectUsing(null)).StringProp);
            Assert.AreEqual("Charlie", ((MockCmObject)((IManyOnePathSortItem)list[2]).KeyObjectUsing(null)).StringProp);

            // Shuffle and sort again (to ensure cache hits are working correctly)
            var temp = list[0];
            list[0] = list[2];
            list[2] = temp;

            sorter.Sort(list);
            Assert.AreEqual("Alpha", ((MockCmObject)((IManyOnePathSortItem)list[0]).KeyObjectUsing(null)).StringProp);
            Assert.AreEqual("Bravo", ((MockCmObject)((IManyOnePathSortItem)list[1]).KeyObjectUsing(null)).StringProp);
            Assert.AreEqual("Charlie", ((MockCmObject)((IManyOnePathSortItem)list[2]).KeyObjectUsing(null)).StringProp);
        }
    }
}
