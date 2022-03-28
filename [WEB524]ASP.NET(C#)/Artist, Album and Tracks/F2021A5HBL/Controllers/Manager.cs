// **************************************************
// WEB524 Project Template V2 == fbf5ea79-2870-4d7b-b645-5e9ddbd419e4
// Do not change this header.
// **************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using F2021A5HBL.Models;
using System.Security.Claims;
using F2021A5HBL.EntityModels;

namespace F2021A5HBL.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailViewModel>();
                cfg.CreateMap<AlbumBaseViewModel, AlbumAddFormViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailViewModel>();
                cfg.CreateMap<ArtistBaseViewModel, ArtistAddFormViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            var genre = from g in ds.Genres
                        orderby g.Name
                        select g;

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(genre);
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artist = from a in ds.Artists
                         orderby a.Name
                         select a;

            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artist);
        }

        public ArtistWithDetailViewModel ArtistGetById(int? id)
        {

            var obj = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);


            if (obj == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Artist, ArtistWithDetailViewModel>(obj);
                
                result.AlbumNames = obj.Albums.Select(a => a.Name);

                return result;
            }

        }
        public ArtistWithDetailViewModel ArtistAdd(ArtistAddFormViewModel newArtist)
        {
            
            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newArtist));
            addedItem.Executive = HttpContext.Current.User.Identity.Name;
            ds.SaveChanges();

            return (addedItem == null) ? null : mapper.Map<Artist, ArtistWithDetailViewModel>(addedItem);
        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var album = from a in ds.Albums
                        orderby a.Name
                        select a;

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(album);
        }

        public AlbumWithDetailViewModel AlbumGetById(int? id)
        {
            //Attempt to fetch the object
            var obj = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(a => a.Id == id);

            if (obj == null)
            {
                return null;
            }
            else
            {

                var result = mapper.Map<Album, AlbumWithDetailViewModel>(obj);
                result.ArtistNames = obj.Artists.Select(a => a.Name);
                return result;

            }
        }

        public AlbumWithDetailViewModel AlbumAdd(AlbumAddFormViewModel newItem)
        {
            var artistIds = newItem.ArtistIds.ToList();
            artistIds.Add(newItem.ArtistId);
            //artist ids + current added artist id

            newItem.ArtistIds = artistIds; 
            //update the new Item's artist ids

            var selectedArtists = new List<Artist>();
            //for IEnumearabl<Artist> 

            foreach (var artistId in newItem.ArtistIds)
            {
                var artist = ds.Artists.Find(artistId);

                if (artist != null)
                {
                    selectedArtists.Add(artist); 
                }
            }//find the matched artist and add to the artist object

            if (selectedArtists.Count() > 0)
            {
                if (newItem.TrackIds.Count() > 0)
                {
                    var selectedTracks = new List<Track>();

                    foreach (var trackId in newItem.TrackIds)
                    {
                        var track = ds.Tracks.Find(trackId);
                        if (track != null)
                        {
                            selectedTracks.Add(track);
                        }
                    }

                    newItem.Tracks = selectedTracks;
                }

                newItem.Artists = selectedArtists;


                newItem.Coordinator = HttpContext.Current.User.Identity.Name;

                var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                ds.SaveChanges();

                return (addedAlbum != null) ? mapper.Map<Album, AlbumWithDetailViewModel>(addedAlbum) : null;
            }
            else
            {
                return null;
            }
            

        }
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = from t in ds.Tracks
                        orderby t.Name
                        select t;

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int? id)
        {
            //fetch the artist
            var artist = ds.Artists.Include("Albums.Tracks").SingleOrDefault(a => a.Id == id);

            if (artist == null)
            {
                return null;
            }
            var tracks = new List<Track>();

            
            foreach (var album in artist.Albums)
            {
                tracks.AddRange(album.Tracks);
            }

            tracks = tracks.Distinct().ToList();

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(tracks.OrderBy(t => t.Name));
        }

        public TrackWithDetailViewModel TrackGetById(int? id)
        {
            
            var track = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t => t.Id == id);

            if (track == null)
            {
                return null;
            }
            else
            {

                var result = mapper.Map<Track, TrackWithDetailViewModel>(track);
                result.AlbumNames = track.Albums.Select(a => a.Name);
                return result;
            }
        }

        public TrackBaseViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            var album = ds.Albums.Find(newTrack.AlbumId);

            if (album != null)
            {
                newTrack.Albums = new List<Album> { album };
            }

            newTrack.Clerk = HttpContext.Current.User.Identity.Name;
            var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

            ds.SaveChanges();
            return (addedTrack != null) ? mapper.Map<Track, TrackWithDetailViewModel>(addedTrack) : null;
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {//add data to here
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                var adminRole = new RoleClaim()
                {
                    Name = "Admin"
                };
                ds.RoleClaims.Add(adminRole);
                var execRole = new RoleClaim()
                {
                    Name = "Executive"
                };
                ds.RoleClaims.Add(execRole);
                var coordRole = new RoleClaim()
                {
                    Name = "Coordinator"
                };
                ds.RoleClaims.Add(coordRole);
                var clerkRole = new RoleClaim()
                {
                    Name = "Clerk"
                };
                ds.RoleClaims.Add(clerkRole);
                var staffRole = new RoleClaim()
                {
                    Name = "Staff"
                };
                ds.RoleClaims.Add(staffRole);

                ds.SaveChanges();
                done = true;
            }


            return done;
        }
        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() > 0) { return false; }
                ds.Genres.Add(new Genre { Name = "Kpop" });
                ds.Genres.Add(new Genre { Name = "Ballad" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Blues" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Funk" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "Techno" });
                ds.Genres.Add(new Genre { Name = "Disco" });
                ds.Genres.Add(new Genre { Name = "R&B" });
            

            ds.SaveChanges();

            return true;
        }
        public bool LoadDataArtist()
        {
            var exec = HttpContext.Current.User.Identity.Name;
            var genreKpop = ds.Genres.SingleOrDefault(g => g.Name == "Kpop");
            var genrePop = ds.Genres.SingleOrDefault(g => g.Name == "Pop");
            var genreRock = ds.Genres.SingleOrDefault(g => g.Name == "Rock");

            if (ds.Artists.Count() > 0) { return false; }

            if (ds.Artists.Count() == 0)
            {
                ds.Artists.Add(new Artist
                {
                    Name = "BlackPink",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/24/Blackpink_PUBG_210321.jpg/450px-Blackpink_PUBG_210321.jpg",
                    BirthName = "BlackPink",
                    BirthOrStartDate = new DateTime(2019, 08, 14),
                    Executive = exec,
                    Genre = genreKpop.Name,
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/1280px-Adele_2016.jpg",
                    BirthName = "Adele Laurie Blue Adkins",
                    BirthOrStartDate = new DateTime(1988, 05, 05),
                    Executive = exec,
                    Genre = genrePop.Name,
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Ed Sheeran",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Ed_Sheeran-6886_%28cropped%29.jpg/330px-Ed_Sheeran-6886_%28cropped%29.jpg",
                    BirthName = "Edward Christopher Sheeran",
                    BirthOrStartDate = new DateTime(1991, 02, 17),
                    Executive = exec,
                    Genre = genreRock.Name,
                });

            }
            
            ds.SaveChanges();

            return true;
        }
        public bool LoadDataAlbum()
        {
            var coord = HttpContext.Current.User.Identity.Name;
            var genreKpop = ds.Genres.SingleOrDefault(g => g.Name == "Kpop");
            var genrePop = ds.Genres.SingleOrDefault(g => g.Name == "Pop");
            var genreRock = ds.Genres.SingleOrDefault(g => g.Name == "Rock");

            var adele = ds.Artists.SingleOrDefault(a => a.Name == "Adele");
            var blackpink = ds.Artists.SingleOrDefault(a => a.Name == "BlackPink");
            var edSheeran = ds.Artists.SingleOrDefault(a => a.Name == "Ed Sheeran");

            if (ds.Albums.Count() == 0)
            {
                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { adele },
                    Name = "25",
                    Genre = genrePop.Name,
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/9/96/Adele_-_25_%28Official_Album_Cover%29.png",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2015, 11, 20),
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { adele },
                    Name = "30",
                    Genre = genrePop.Name,
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/7/76/Adele_-_30.png",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2021, 11, 19)
                });


                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { blackpink },
                    Name = "BlackPink in Your Area",
                    Genre = genreKpop.Name,
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/3/33/Black_Pink_Black_In_Your_Area_Digital_Cover.jpg",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2018, 11, 23)

                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { blackpink },
                    Name = "The Album",
                    Genre = genreKpop.Name,
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/f/f2/BLACKPINK-_The_Album.png",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2020, 10, 02)

                });


                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { edSheeran },
                    Name = "%",
                    Genre = genreRock.Name,
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/4/45/Divide_cover.png",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2017, 03, 03)

                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { edSheeran },
                    Name = "=",
                    Genre = genreRock.Name,
                    UrlAlbum = "https://media.pitchfork.com/photos/6179a8ac8863d4d56b12af42/1:1/w_320/100000x100000-999.jpg",
                    Coordinator = coord,
                    ReleaseDate = new DateTime(2021, 10, 29)

                });
            }

            ds.SaveChanges();

            return true;
        }
        public bool LoadDataTrack()
        {
            
                var clerk = HttpContext.Current.User.Identity.Name;
                var genreKpop = ds.Genres.SingleOrDefault(g => g.Name == "Kpop");
                var genrePop = ds.Genres.SingleOrDefault(g => g.Name == "Pop");
                var genreRock = ds.Genres.SingleOrDefault(g => g.Name == "Rock");

                if (ds.Tracks.Count() > 0) { return false; }

                var Adele25 = ds.Albums.SingleOrDefault(a => a.Name == "25");
            

                var BPinYourArea = ds.Albums.SingleOrDefault(a => a.Name == "BlackPink in Your Area");
          

            var EdAlbum = ds.Albums.SingleOrDefault(a => a.Name == "%");
           

            if (ds.Tracks.Count() == 0)
                {
                    ds.Tracks.Add(new Track
                    {
                        Name = "Hello",
                        Clerk = clerk,
                        Composers = "Adele Adkins, Greg Kurstin",
                        Genre = genrePop.Name,
                        Albums = new List<Album> { Adele25 }
                    });

                    ds.Tracks.Add(new Track
                    {
                        Name = "When We Were Young",
                        Clerk = clerk,
                        Composers = "AdkinsTobias, Jesso Jr.",
                        Genre = genrePop.Name,
                        Albums = new List<Album> { Adele25 }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Water Under the Bridge",
                        Clerk = clerk,
                        Composers = "Adkins, Kurstin",
                        Genre = genrePop.Name,
                        Albums = new List<Album> { Adele25 }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "All I Ask",
                        Clerk = clerk,
                        Composers = "Brody Brown",
                        Genre = genrePop.Name,
                        Albums = new List<Album> { Adele25 }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Love in Dark",
                        Clerk = clerk,
                        Composers = "Adkins, Samuel Dixon",
                        Genre = genrePop.Name,
                        Albums = new List<Album> { Adele25 }
                    });


                    ds.Tracks.Add(new Track
                    {
                        Name = "Boombayah",
                        Clerk = clerk,
                        Composers = "Teddy, Rebecca Johnson, Sunny Boy",
                        Genre = genreKpop.Name,
                        Albums = new List<Album> { BPinYourArea }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Whistle",
                        Clerk = clerk,
                        Composers = "Teddy, Rebecca Johnson, Sunny Boy, B.I",
                        Genre = genreKpop.Name,
                        Albums = new List<Album> { BPinYourArea }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Stay",
                        Clerk = clerk,
                        Composers = "Teddy, Emyli",
                        Genre = genreKpop.Name,
                        Albums = new List<Album> { BPinYourArea }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Ddu-Du Ddu-Du",
                        Clerk = clerk,
                        Composers = "Teddy, Sunny Boy",
                        Genre = genreKpop.Name,
                        Albums = new List<Album> { BPinYourArea }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Forever Young",
                        Clerk = clerk,
                        Composers = "Teddy, Emyli, ZERO",
                        Genre = genreKpop.Name,
                        Albums = new List<Album> { BPinYourArea }
                    });


                    ds.Tracks.Add(new Track
                    {
                        Name = "Eraser",
                        Clerk = clerk,
                        Composers = "Ed Sheeran, Johnny McDaid",
                        Genre = genreRock.Name,
                        Albums = new List<Album> { EdAlbum }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Dive",
                        Clerk = clerk,
                        Composers = "Benny Blanco, Ed Sheeran, Julia Michaels",
                        Genre = genreRock.Name,
                        Albums = new List<Album> { EdAlbum }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Shape of You",
                        Clerk = clerk,
                        Composers = "Ed Sheeran, Johnny McDaid, Kandi Burruss, Kevin She'kspere Briggs, Steve Mac, Tameka Cottle",
                        Genre = genreRock.Name,
                        Albums = new List<Album> { EdAlbum }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Perfect",
                        Clerk = clerk,
                        Composers = "Ed Sheeran",
                        Genre = genreRock.Name,
                        Albums = new List<Album> { EdAlbum }
                    });
                    ds.Tracks.Add(new Track
                    {
                        Name = "Happier",
                        Clerk = clerk,
                        Composers = "Benny Blanco, Ed Sheeran, Ryan Tedder",
                        Genre = genreRock.Name,
                        Albums = new List<Album> { EdAlbum }
                    });


                }

                ds.SaveChanges();

                return true;
           
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveDataTracks()
        {
            try
            {
                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveDataAlbums()
        {
            try
            {
                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveDataArtists()
        {
            try
            {
                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveDataGenres()
        {
            try
            {
                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}