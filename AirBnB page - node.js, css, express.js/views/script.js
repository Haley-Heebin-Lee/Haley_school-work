window.onscroll = function() {
    var stickybar = document.getElementById('stickybar');
    var sticky = stickybar.offsetTop;
    if (window.pageYOffset >= sticky) {
      stickybar.classList.add('sticky');
    } else {
      stickybar.classList.remove('sticky');
    }
  };

/*window.onload = () => {
  var modal = document.getElementById("myModal");
  var btn = document.getElementById("myBtn");
  var span = document.getElementsByClassName("close")[0];
  
  btn.onclick = function() {
    modal.style.display = "block";
  }
  span.onclick = function() {
    modal.style.display = "none";
  }
  window.onclick = function(event) {
    if (event.target == modal) {
      modal.style.display = "none";
    }
  }
}*/