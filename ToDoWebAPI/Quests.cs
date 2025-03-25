namespace ToDoWebAPI
{
    using System.ComponentModel.DataAnnotations;
    public class Quest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Title cannot be longer than 20 characters")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string? Description { get; set; } // ? means can be nullable

        public bool? IsComplete { get; set; }

        public DateTime? DueDate { get; set; } // ? means can be nullable

    }
}
