// **************************************************
// WEB524 Project Template V1 == 3e99e29e-7777-4663-8840-1e37084d9dd4
// Do not change this header.
// **************************************************

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using F2021A3HBL.EntityModels;
using F2021A3HBL.Models;

namespace F2021A3HBL.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                //===============================

                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackBaseViewModel, TrackAddFormViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, TrackAddFormViewModel>();
                //================================
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistWithDetailsViewModel>();
                cfg.CreateMap<PlaylistBaseViewModel, PlaylistEditTracksFormViewModel>();
                cfg.CreateMap<PlaylistWithDetailsViewModel, PlaylistEditTracksFormViewModel>();


            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add your methods and call them from controllers.  Use the suggested naming convention.
        // Ensure that your methods accept and deliver ONLY view model objects and collections.
        // When working with collections, the return type is almost always IEnumerable<T>.
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.OrderBy(n => n.Title);

            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = ds.Artists.OrderBy(n => n.Name);
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artists);
        }
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var media = ds.MediaTypes.OrderBy(n => n.Name);
            return mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(media);
        }
        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var playlists = ds.Playlists.Include("Tracks").OrderBy(n => n.Name);
            return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(playlists);
        }
        public PlaylistWithDetailsViewModel PlaylistGetById(int id)
        {
            var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == id);
            return (playlist == null) ? null : mapper.Map<Playlist, PlaylistWithDetailsViewModel>(playlist);
        }
        public PlaylistWithDetailsViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newItem)
        {
            var playlist = ds.Playlists.Include("Tracks").SingleOrDefault(p => p.PlaylistId == newItem.Id);

            if(playlist == null)
            {
                return null;
            }
            else
            {
                playlist.Tracks.Clear();
                foreach(var item in newItem.TrackId)
                {
                    var track = ds.Tracks.Find(item);
                    playlist.Tracks.Add(track);
                }
                ds.SaveChanges();

                return mapper.Map<Playlist, PlaylistWithDetailsViewModel>(playlist);
            }
        }
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks);
        }
        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var tracks = ds.Tracks.Include("MediaType").Include("Album.Artist").OrderBy(n=>n.Name);
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(tracks);
        }
        
        public TrackWithDetailViewModel TrackGetByIdWithDetail(int id)
        {
            var track = ds.Tracks.Include("MediaType").Include("Album.Artist").SingleOrDefault(ln => ln.TrackId == id);
            return (track == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(track);
        }
        public TrackWithDetailViewModel TrackAdd(TrackAddFormViewModel newItem)
        {
            var album = ds.Albums.Find(newItem.AlbumId);
            var media = ds.MediaTypes.Find(newItem.MediaTypeId);

            if(album == null)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));
                addedItem.Album = album;
                addedItem.MediaType = media;

                ds.SaveChanges();
                return (addedItem == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedItem);
            }
        }
    }
}