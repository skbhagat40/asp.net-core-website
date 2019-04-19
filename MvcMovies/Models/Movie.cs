using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovies.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } 
        public string Plot { get; set; }
        public byte[] Poster { get; set; }
       // public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectList> Choices { get; set; }
       
        public virtual ActorMovie ActorMovie { get; set; }
        public  ICollection<ActorMovie> ActorMovies { get; set; }
       
        public string ProducerName { get; set; }
        public int ProducerId { get; set; }
        public int ActorId { get; set; }
        public virtual Producer Producer { get; set; }
        public string SelectedActors { get; set; }
    }
    public class Actor
    {
        public int ActorID { get; set; }
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public string Name { get; set; }
        public virtual ActorMovie ActorMovie { get; set; }
        public  IEnumerable<ActorMovie> ActorMovies { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
    }
    public class ActorMovie
    {
     
        public int ActorId { get; set; }
       
        public Actor Actor { get; set; }
       
        public int MovieId { get; set; }
        
        public Movie Movie { get; set; }
    }
    public class Producer
    {
        
        public int ProducerID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
       
        public ICollection<Movie> Movies { get; set; }
        public int MovieId { get; set; }
        
    }
}
