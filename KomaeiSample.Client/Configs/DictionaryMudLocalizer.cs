namespace KomaeiSample.Client.Configs;
public class DictionaryMudLocalizer : MudLocalizer
{
    private Dictionary<string, string> _localization;

    public DictionaryMudLocalizer()
    {
        _localization = new()
        {
            { "MudDataGrid_Unsort", "حذف سورت" },
            { "MudDataGrid_Filter", "فیلتر" },
            { "MudDataGrid_Clear", "حذف فیلتر" },
            { "MudDataGrid_Contains", "شامل باشد" },
            { "MudDataGrid_NotContains", "شامل نباشد" },
            { "MudDataGrid_Equals", "برابر باشد" },
            { "MudDataGrid_NotEquals", "برابر نباشد" },
            { "MudDataGrid_StartsWith", "شروع شود با" },
            { "MudDataGrid_EndsWith", "پایان یابد با" },
            { "MudDataGrid_IsEmpty", "خالی باشد" },
            { "MudDataGrid_IsNotEmpty", "خالی نباشد" },
            { "MudDataGrid_FilterValue", "" },
        };
    }

    public override LocalizedString this[string key]
    {
        get
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Parent.TwoLetterISOLanguageName;
            if (currentCulture.Equals("fa", StringComparison.InvariantCultureIgnoreCase)
                && _localization.TryGetValue(key, out var res))
            {
                return new(key, res);
            }
            else
            {
                return new(key, key, true);
            }
        }
    }
}