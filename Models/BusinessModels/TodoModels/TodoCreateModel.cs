namespace TodoApi.Models.TodoModel
{
    using System;
    using TodoApi.Enums;

    /// <summary>
    /// a class describe create model
    /// </summary>
    public class TodoCreateModel
    {
        /// <summary>
        /// the title of TODO
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// the description of TODO
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// the date of TODO
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// the importance of TODO
        /// </summary>
        public ImportanceLevel ImportanceLevel { get; set; }
    }
}
