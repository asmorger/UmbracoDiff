namespace UmbracoDiff.Events
{
    public class UmbracoConnectionRemovedEvent
    {
        public string Name { get; set; }

        public UmbracoConnectionRemovedEvent(string name)
        {
            this.Name = name;
        }
    }
}
