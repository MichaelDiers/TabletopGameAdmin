﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Md.Tga.CreateGameMailSubscriber {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class GameTerminationUpdateText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GameTerminationUpdateText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Md.Tga.CreateGameMailSubscriber.GameTerminationUpdateText", typeof(GameTerminationUpdateText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;de&quot;&gt;
        ///    &lt;head&gt;
        ///        &lt;meta charset=&quot;utf-8&quot;&gt;
        ///        &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width&quot; initial-scale=&quot;1&quot;&gt;
        ///        &lt;meta name=&quot;google&quot; content=&quot;notranslate&quot;&gt;
        ///    &lt;/head&gt;
        ///    &lt;body&gt;
        ///        &lt;h1&gt;Hej {0}!&lt;/h1&gt;
        ///        &lt;p&gt;Das aktuelle Meinungsbild für eine Beendigung des Spiels {1}..&lt;/p&gt;
        ///        &lt;ul&gt;{2}&lt;/ul&gt;
        ///        &lt;p&gt;Viele Grüße,&lt;br&gt;&lt;br&gt;{3}&lt;/p&gt;
        ///    &lt;/body&gt;
        ///&lt;/html&gt;.
        /// </summary>
        public static string BodyHtml {
            get {
                return ResourceManager.GetString("BodyHtml", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;li&gt;{0} - {1}&lt;/li&gt;.
        /// </summary>
        public static string BodyHtmlResult {
            get {
                return ResourceManager.GetString("BodyHtmlResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hej {0}!
        ///
        ///Das aktuelle Meinungsbild für eine Beendigung des Spiels {1}.
        ///
        ///{2}
        ///
        ///Viele Grüße,
        ///
        ///{3}.
        /// </summary>
        public static string BodyText {
            get {
                return ResourceManager.GetString("BodyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - {0}: {1}
        ///.
        /// </summary>
        public static string BodyTextResult {
            get {
                return ResourceManager.GetString("BodyTextResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nicht abgestimmt.
        /// </summary>
        public static string Neutral {
            get {
                return ResourceManager.GetString("Neutral", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neues aus der Diplomatie.
        /// </summary>
        public static string Subject {
            get {
                return ResourceManager.GetString("Subject", resourceCulture);
            }
        }
    }
}
