// Output.cs created with MonoDevelop at 4:40 PM 1/29/2009
// @author mbirth

using System;

namespace vampi {
    public abstract class Output {
        public bool requestAbort = false;
        
        public Output() {
        }
        
        public virtual void doOutput() {
        }
    }
}
