using System.Collections.Generic;


namespace StarkInfra
{
    /// <summary>
    /// StarkInfra.TemplateReplacements object
    /// <br/>
    /// The StarkInfra.TemplateReplacements object carries the custom values used to fill the placeholders of the contract template of a CreditNote.
    /// Its keys are free, they depend on the template, and are sent exactly as written.
    /// It is a dictionary of string keys, so every key received from the API is kept.
    /// Null values are sent as JSON null and values, including DateTime, are serialized as-is, without the SDK's date conversion.
    /// <br/>
    /// Parameters (optional):
    /// <list>
    ///     <item>values [dictionary, default empty]: placeholder name and its value. ex: new TemplateReplacements(new Dictionary<string, object>{ { "guarantorName", "Jon Snow" } })</item>
    /// </list>
    /// </summary>
    public class TemplateReplacements : Dictionary<string, object>
    {
        public TemplateReplacements() { }

        public TemplateReplacements(IDictionary<string, object> values) : base(values) { }
    }
}
