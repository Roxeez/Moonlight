namespace NtCore.Game.Data
{
    public class SpPointInfo
    {
        /// <summary>
        /// Current sp points
        /// </summary>
        public int SpPoints { get; internal set; }
        
        /// <summary>
        /// Additional sp points
        /// </summary>
        public int AdditionalSpPoints { get; internal set; }
        
        /// <summary>
        /// Maximum sp points
        /// </summary>
        public int MaximumSpPoints { get; internal set; }
        
        /// <summary>
        /// Maximum additional sp points
        /// </summary>
        public int MaximumAdditionalSpPoints { get; internal set; }
    }
}