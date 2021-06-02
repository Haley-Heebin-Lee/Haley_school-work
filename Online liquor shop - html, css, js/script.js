/* Add any JavaScript you need to this file. */
var sojulist = [
  //all size is 360ml
  {
    name: 'chamisul',
    alchohol: '16.9%',
    desc:
      'Chamisul Fresh, which has become more trendy with a more soothness after filtering out 4 times with bamboo charcoal',
    price: 6.2,
    availability: true,
    sale: false
  },
  {
    name: 'chamisul-grapefruit',
    alchohol: '13%',
    desc:
      'With a perfect blend of slender fruit grapefruit and clean dew, anyone can enjoy it lightly',
    price: 7.5,
    availability: true,
    sale: true
  },
  {
    name: 'chumchurum',
    alchohol: '16.9%',
    desc:
      'Clear alcohol made from rice and other cereal distillation served fresh and it offers lacteous and anise notes',
    price: 6.4,
    availability: true,
    sale: true
  },
  {
    name: 'IseulTokTok',
    alchohol: '3%',
    desc: 'Pop! Pop! A refresh peach flavor that spreads in your mouth with sparkling soda',
    price: 5.5,
    availability: true,
    sale: false
  },
  {
    name: 'soonhari-apple',
    alchohol: '12%',
    desc: 'Pop! Pop! A refresh peach flavor that spreads in your mouth with sparkling soda',
    price: 7.6,
    availability: true,
    sale: false
  },
  {
    name: 'soonhari-citron',
    alchohol: '14%',
    desc: 'properly mixed with citron juice and can be enjoyed fresh and deliciously',
    price: 7.6,
    availability: false,
    sale: false
  }
];
var beerlist = [
  //all size is 330ml, type is larger
  {
    name: 'cass',
    alchohol: '4.5%',
    desc: 'Improved the fresh and pungent taste of beer with cold-Filtered processing',
    price: 3.4,
    availability: true,
    sale: false
  },
  {
    name: 'hite',
    alchohol: '4.5%',
    desc:
      'The upgraded extra-cold method maximizes the original cool and refreshing taste of lager beer',
    price: 4.0,
    availability: true,
    sale: false
  },
  {
    name: 'kloud',
    alchohol: '5%',
    desc:
      " Applied the 'Original Gravity' technique that don't add water to the undiluted solution of beer fermentation to enhance the taste of deep and rich premium beer",
    price: 4.1,
    availability: true,
    sale: true
  },
  {
    name: 'max',
    alchohol: '4.5%',
    desc: 'Deep and rich cream All Malt Beer!',
    price: 3.8,
    availability: true,
    sale: false
  }
];
var rwlist = [
  //all size is 750ml + warning expire is short
  {
    name: 'saengmaggeolli',
    alchohol: '6%',
    desc: 'Has a lot of essential amino acids with non-heating raw rice fermentation method',
    price: 6.5,
    availability: true,
    sale: true
  },
  {
    name: 'pinenut-maggeolli',
    alchohol: '6%',
    desc: 'Anyone can drink with unique aromatic taste of pine nuts in it',
    price: 7.0,
    availability: true,
    sale: false
  },
  {
    name: 'corn-maggeolli',
    alchohol: '6%',
    desc: 'Popular type of maggeolli with refreshing and sweet taste',
    price: 6.9,
    availability: true,
    sale: true
  },
  {
    name: 'chestnut-maggeolli',
    alchohol: '7%',
    desc: 'A suitable combination of rice and wheat flour with the sweetness of chestnut',
    price: 6.6,
    availability: true,
    sale: false
  }
];
var otherlist = [
  //all size is 750ml + no sale
  {
    name: 'bokbunjaju',
    alchohol: '17%',
    desc:
      'This bokbunja, which has been fermented for more than 30 days and aged for more than a year, has the unique color, aroma and taste of bokbunja. It is highly flavored because it ferments 100% of the bokbunja undiluted solution',
    price: 19.9,
    amount: '375ml',
    availability: true,
    sale: false
  },
  {
    name: 'korea-ginseng-wine',
    alchohol: '25%',
    desc:
      "Korea's traditional liquor produced by fermenting ginseng at low temperatures for more than five years with excellent medicinal properties",
    price: 29.5,
    amount: '375ml',
    availability: true,
    sale: false
  },
  {
    name: 'seoljungmae',
    alchohol: '14%',
    desc: 'The whole plums inside make it sweeter and smell stronger',
    price: 10.9,
    amount: '360ml',
    availability: true,
    sale: false
  }
];
//when scroll down, the bar on the top sticks to the page.
window.onscroll = function() {
  var stickybar = document.getElementById('stickybar');
  var sticky = stickybar.offsetTop;
  if (window.pageYOffset >= sticky) {
    stickybar.classList.add('sticky');
  } else {
    stickybar.classList.remove('sticky');
  }
};

window.onload = () => {
  var body = document.body;
  var main = document.querySelector('main');
  //======the main page shows what's on sale===========

  var main_sale = document.createElement('main');
  main_sale = mainsale(main_sale);
  main.appendChild(main_sale);

  //==============home=====================
  var home = document.querySelector('#home');
  home.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = mainsale(maincontents);
    main.appendChild(maincontents);
  };
  //==============soju list==============
  var soju = document.querySelector('#soju');
  soju.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = displaySoju(maincontents);
    main.appendChild(maincontents);
  };
  //================beer list=============
  var beer = document.querySelector('#beer');
  beer.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = displayBeer(maincontents);
    main.appendChild(maincontents);
  };

  //================ricewine list=============
  var rw = document.querySelector('#ricewine');
  rw.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = displayRW(maincontents);
    main.appendChild(maincontents);
  };

  //================others list=============
  var others = document.querySelector('#others');
  others.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = displayOthers(maincontents);
    main.appendChild(maincontents);
  };

  //==============log in ===================
  var login = document.querySelector('#login');
  login.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = loginF(maincontents);
    main.appendChild(maincontents);
  };
  //=============About===================
  var location = document.querySelector('#about');
  location.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = getAbout(maincontents);
    main.appendChild(maincontents);
  };

  //=============location===================
  var location = document.querySelector('#location');
  location.onclick = function() {
    var isExistmain = body.querySelectorAll('main');
    var second = isExistmain[1];
    if (second) main.removeChild(second);

    var maincontents = document.createElement('main');
    maincontents = getLocation(maincontents);
    main.appendChild(maincontents);
  };
};
//display others
function displayOthers(main) {
  let header = document.createElement('p');
  header.className = 'header';
  header.innerText = 'Korean Traditional fruit Liquor and medicinal liquor';
  let p = document.createElement('p');
  p.className = 'capacity';
  p.innerText = 'Every capacity is 750ml';
  p.appendChild(document.createElement('hr'));

  let table = document.createElement('table');
  let tb = document.createElement('tbody');

  for (let i = 0; i < otherlist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');

    td1.appendChild(getImg(otherlist[i].name, 'others'));
    td1.appendChild(getName(otherlist[i].name));
    td2.appendChild(getDesc(otherlist[i].desc, otherlist[i].availability));
    td1.appendChild(getAlcohol(otherlist[i].alchohol));
    td1.appendChild(getPrice(otherlist[i].price, otherlist[i].sale));

    tr.appendChild(td1);
    tr.appendChild(td2);
    tb.appendChild(tr);
  }
  table.appendChild(tb);
  main.appendChild(header);
  main.appendChild(p);
  main.appendChild(table);

  return main;
}

//display ricewine
function displayRW(main) {
  let header = document.createElement('p');
  header.className = 'header';
  header.innerText = 'The milky, off-white and lightly sparkling rice wine';
  let p = document.createElement('p');
  p.className = 'capacity';
  p.innerText = 'Every capacity is 750ml. Note that the expiry date is short';
  p.appendChild(document.createElement('hr'));

  let table = document.createElement('table');
  let tb = document.createElement('tbody');

  for (let i = 0; i < rwlist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');

    td1.appendChild(getImg(rwlist[i].name, 'ricewine'));
    td1.appendChild(getName(rwlist[i].name));
    td2.appendChild(getDesc(rwlist[i].desc, rwlist[i].availability));
    td1.appendChild(getAlcohol(rwlist[i].alchohol));
    td1.appendChild(getPrice(rwlist[i].price, rwlist[i].sale));

    tr.appendChild(td1);
    tr.appendChild(td2);
    tb.appendChild(tr);
  }
  table.appendChild(tb);
  main.appendChild(header);
  main.appendChild(p);
  main.appendChild(table);

  return main;
}

//display beer
function displayBeer(main) {
  let header = document.createElement('p');
  header.className = 'header';
  header.innerText = 'Smooth Larger beer made in Korea';
  let p = document.createElement('p');
  p.className = 'capacity';
  p.innerText = 'Every capacity is 330ml and type is larger';
  p.appendChild(document.createElement('hr'));

  let table = document.createElement('table');
  let tb = document.createElement('tbody');

  for (let i = 0; i < beerlist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');

    td1.appendChild(getImg(beerlist[i].name, 'beer'));
    td1.appendChild(getName(beerlist[i].name));
    td2.appendChild(getDesc(beerlist[i].desc, beerlist[i].availability));
    td1.appendChild(getAlcohol(beerlist[i].alchohol));
    td1.appendChild(getPrice(beerlist[i].price, beerlist[i].sale));

    tr.appendChild(td1);
    tr.appendChild(td2);
    tb.appendChild(tr);
  }
  table.appendChild(tb);
  main.appendChild(header);
  main.appendChild(p);
  main.appendChild(table);

  return main;
}

//display soju
function displaySoju(main) {
  let header = document.createElement('p');
  header.className = 'header';
  header.innerText = 'Traditionally made from rice, wheat, or barley';
  let p = document.createElement('p');
  p.className = 'capacity';
  p.innerText = 'Every capacity is 360ml';
  p.appendChild(document.createElement('hr'));

  let table = document.createElement('table');
  let tb = document.createElement('tbody');

  for (let i = 0; i < sojulist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');

    td1.appendChild(getImg(sojulist[i].name, 'soju'));
    td1.appendChild(getName(sojulist[i].name));
    td2.appendChild(getDesc(sojulist[i].desc, sojulist[i].availability));
    td1.appendChild(getAlcohol(sojulist[i].alchohol));
    td1.appendChild(getPrice(sojulist[i].price, sojulist[i].sale));

    tr.appendChild(td1);
    tr.appendChild(td2);
    tb.appendChild(tr);
  }
  table.appendChild(tb);
  main.appendChild(header);
  main.appendChild(p);
  main.appendChild(table);

  return main;
}

//main menu
function mainsale(main_sale) {
  let h1 = document.createElement('h1');
  h1.className = 'main-sale';
  h1.innerText = 'Welcome to our online store!';
  let h3 = document.createElement('h3');
  h3.className = 'whatonsale';
  h3.innerText = "What's on sale";
  h3.appendChild(document.createElement('hr'));

  let table = document.createElement('table');
  let tb = document.createElement('tbody');

  for (let i = 0; i < sojulist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');
    if (sojulist[i].sale === true && sojulist[i].availability === true) {
      td1.appendChild(getImg(sojulist[i].name, 'soju'));
      td1.appendChild(getName(sojulist[i].name));
      td2.appendChild(getDesc(sojulist[i].desc));
      td1.appendChild(getAlcohol(sojulist[i].alchohol));
      td1.appendChild(getPrice(sojulist[i].price, true));

      tr.appendChild(td1);
      tr.appendChild(td2);
      tb.appendChild(tr);
    }
  }
  for (let i = 0; i < beerlist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');
    if (beerlist[i].sale === true && beerlist[i].availability === true) {
      td1.appendChild(getImg(beerlist[i].name, 'beer'));
      td1.appendChild(getName(beerlist[i].name));
      td2.appendChild(getDesc(beerlist[i].desc));
      td1.appendChild(getAlcohol(beerlist[i].alchohol));
      td1.appendChild(getPrice(beerlist[i].price, true));

      tr.appendChild(td1);
      tr.appendChild(td2);
      tb.appendChild(tr);
    }
  }
  for (let i = 0; i < rwlist.length; i++) {
    let tr = document.createElement('tr');
    let td1 = document.createElement('td');
    let td2 = document.createElement('td');
    if (rwlist[i].sale === true && rwlist[i].availability === true) {
      td1.appendChild(getImg(rwlist[i].name, 'ricewine'));
      td1.appendChild(getName(rwlist[i].name));
      td2.appendChild(getDesc(rwlist[i].desc));
      td1.appendChild(getAlcohol(rwlist[i].alchohol));
      td1.appendChild(getPrice(rwlist[i].price, true));

      tr.appendChild(td1);
      tr.appendChild(td2);
      tb.appendChild(tr);
    }
  }
  table.appendChild(tb);

  main_sale.appendChild(h1);
  main_sale.appendChild(h3);
  main_sale.appendChild(table);

  return main_sale;
}
//get name
function getName(name) {
  let h3 = document.createElement('h3');
  h3.className = 'Name';
  h3.appendChild(document.createTextNode(name));
  return h3;
}
//get desc
function getDesc(text, availability = true) {
  let p = document.createElement('p');
  p.className = 'Desc';
  p.appendChild(document.createTextNode(text));
  if (availability === false) {
    let p2 = document.createElement('p');
    p2.className = 'NotAvailable';
    let no = document.createTextNode('<Currnetly not available>');
    p2.appendChild(no);

    p.appendChild(p2);
  }

  return p;
}
//get Alchohol %
function getAlcohol(text) {
  let p = document.createElement('p');
  p.className = 'Alchohol';
  text = 'Alc%: ' + text;
  p.appendChild(document.createTextNode(text));
  return p;
}
//get Price
function getPrice(text, TorF) {
  let p = document.createElement('p');
  p.className = 'Price';
  if (TorF === true) {
    var reduced = text * 0.8;
    p.className = 'Price-sale';
    reduced = (Math.round(reduced * 100) / 100).toFixed(2);
    text = '$ ' + text + ' -> $ ' + reduced;
  } else {
    text = text.toFixed(2);
    text = '$ ' + text;
  }

  p.appendChild(document.createTextNode(text));
  return p;
}
// get img
function getImg(name, kind) {
  let img = document.createElement('img');
  img.src = 'images/' + kind + '/' + name + '.png';
  img.width = '240';
  img.height = '250';
  img.onerror = function() {
    this.style.display = 'none';
  };
  return img;
}
//About
function getAbout(main) {
  let h3 = document.createElement('h3');
  h3.className = 'about_header';
  h3.innerText = 'Company Profile';
  let hr = document.createElement('hr');
  let div = document.createElement('div');
  div.className = 'about_contents';
  let p1 = document.createElement('p');
  p1.innerText =
    'Korea Liquor Cellars is a wholesaler/distributor of Korean liquors supplying 800 restaurants, hotels, and liquor shops with huge range of Korean Soju, Beer, Wine and other liquors.';
  let p2 = document.createElement('p');
  p2.innerText =
    'Our company was originally founded in Ontario, Canada in 2010, and now we just launched the website';
  let p3 = document.createElement('p');
  p3.innerText =
    'We hope you enjoy your shopping experience and look forward to serving you and we supports the Responsible Service of Alcohol. It is against the law to sell or supply alcohol to, or to obtain alcohol on behalf of, a person under the age of 19 years.';
  let p4 = document.createElement('p');
  p4.innerText = 'Thank you for visiting our website.';

  h3.appendChild(hr);
  main.appendChild(h3);
  div.appendChild(p1);
  div.appendChild(p2);
  div.appendChild(p3);
  div.appendChild(p4);
  main.appendChild(div);
  return main;
}

//location
function getLocation(main) {
  let iframe = document.createElement('iframe');
  iframe.src =
    'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3140.5559684281084!2d-79.35348215504443!3d43.795638188037906!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x138c6f9f7ce96a10!2sSeneca!5e0!3m2!1sko!2sca!4v1596604167673!5m2!1sko!2sca';
  iframe.width = '600';
  iframe.height = '450';
  iframe.frameBorder = '0';
  iframe.style = 'border:0;';
  iframe.tabIndex = '0';
  let div = document.createElement('div');
  div.className = 'location';
  let h2 = document.createElement('h2');
  h2.innerText = 'Korea Liquor in Toronto';
  let p1 = document.createElement('p');
  p1.className = 'loc';
  p1.innerText = '1750 Finch Ave. East';
  let p2 = document.createElement('p');
  p2.className = 'loc';
  p2.innerText = 'Toronto, Ont. M2J 2X5';
  let p3 = document.createElement('p');
  p3.className = 'phoneNo';
  p3.innerText = '647-265-3839 ';
  let spanP = document.createElement('span');
  spanP.className = 'fa fa-phone';
  p3.appendChild(spanP);

  div.appendChild(h2);
  div.appendChild(p1);
  div.appendChild(p2);
  div.appendChild(p3);

  main.appendChild(iframe);
  main.appendChild(div);
  return main;
}

//login dom
function loginF(main) {
  let div1 = document.createElement('div');
  div1.className = 'form-wrapper';
  let form = document.createElement('form');
  form.name = 'registration';
  form.method = 'POST';

  let h1 = document.createElement('h1');
  h1.innerText = 'Welcome Back !';
  let label1 = document.createElement('label');
  label1.setAttribute('for', 'email');
  label1.innerText = 'Email address:';
  let input1 = document.createElement('input');
  input1.type = 'email';
  input1.name = 'email';
  input1.id = 'email';
  input1.autofocus;
  input1.autocomplete = 'email';
  input1.placeholder = 'Enter email address...';

  let label2 = document.createElement('label');
  label2.setAttribute('for', 'password');
  label2.innerText = 'Password:';
  let input2 = document.createElement('input');
  input2.type = 'password';
  input2.name = 'password';
  input2.id = 'password';
  input2.placeholder = 'Enter Password...';

  let button_login = document.createElement('button');
  button_login.type = 'submit';
  button_login.className = 'btn btn-submit';
  button_login.innerText = 'Login';

  let hr1 = document.createElement('hr');

  let btngoogle = document.createElement('button');
  btngoogle.className = 'btn btn-google';
  let spanG = document.createElement('span');
  spanG.className = 'fab fa-google';
  spanG.innerText = ' Login with Google';
  btngoogle.appendChild(spanG);

  let btnfacebook = document.createElement('button');
  btnfacebook.className = 'btn btn-facebook';
  let spanF = document.createElement('span');
  spanF.className = 'fab fa-facebook-f';
  spanF.innerText = ' Login with Facebook';
  btnfacebook.appendChild(spanF);

  let hr2 = document.createElement('hr');

  let div2 = document.createElement('div');
  div2.className = 'ext-url';
  let p1 = document.createElement('p');
  let a1 = document.createElement('a');
  a1.href = 'forgot.html';
  a1.innerText = 'Forgot Password?';
  p1.appendChild(a1);
  let p2 = document.createElement('p');
  let a2 = document.createElement('a');
  a2.href = 'register.html';
  a2.innerText = 'Create an Account!';
  p2.appendChild(a2);
  div2.appendChild(p1);
  div2.appendChild(p2);

  form.appendChild(h1);
  form.appendChild(label1);
  form.appendChild(input1);
  form.appendChild(label2);
  form.appendChild(input2);
  form.appendChild(button_login);
  form.appendChild(hr1);
  form.appendChild(btngoogle);
  form.appendChild(btnfacebook);
  form.appendChild(hr2);
  form.appendChild(div2);
  div1.appendChild(form);
  main.appendChild(div1);

  return main;
}
