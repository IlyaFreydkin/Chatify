<template>
   <footer class="footer">
  	 <div class="container">
  	 	<div class="row">
  	 		<div class="footer-col">
  	 			<h4>CHATIFY</h4>
  	 			<ul>
  	 				<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	Home
                  	</router-link>
                	</li>
  	 				<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/chatroom" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	Chat Room
                  	</router-link>
					</li>
					<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/webrtc" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	WebRTC
                  	</router-link>
					</li>
					<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/service" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	Service
                  	</router-link>
					</li>
					<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/journal" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	Journal
                  	</router-link>
					</li>
					<li class="pl-4 pl-md-0 ml-0 ml-md-4">
                  	<router-link to="/contact" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                    	Contact
                  	</router-link>
					</li>
  	 			</ul>
  	 		</div>
  	 		<div class="footer-col">
  	 			<h4>Informationen</h4>
  	 			<ul>	
  	 				<li><a href="#">Lerne kennen</a></li>
  	 				<li><a href="#">Jetzt sofort</a></li>		
  	 			</ul>
  	 		</div>
            <div class="footer-col">
  	 			<h4>Hilfe</h4>
  	 			<ul>
  	 				
  	 				<li><a href="#">FAQ</a></li>
  	 				<li><a href="#">Impressum</a></li>
  	 				
  	 			</ul>
  	 		</div>
  	 		<div class="footer-col">
  	 			<h4>Folg uns</h4>
  	 			<div class="social-links">
  	 				<a href="#"><i class="fab fa-facebook-f"></i></a>
  	 				<a href="#"><i class="fab fa-twitter"></i></a>
  	 				<a href="#"><i class="fab fa-instagram"></i></a>
  	 				<a href="#"><i class="fab fa-linkedin-in"></i></a>
  	 			</div>
  	 		</div>
  	 	</div>
  	 </div>
  </footer>

</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');
body{
	line-height: 1.5;
	font-family: 'Poppins', sans-serif;
}
*{
	margin:0;
	padding:0;
	box-sizing: border-box;
}
.container{
	max-width: 1170px;
	margin:auto;
}
.row{
	display: flex;
	flex-wrap: wrap;
}
ul{
	list-style: none;
}
.footer{
	margin-top: auto;
	background-color: #725AC1;
    padding: 50px 0;
    position: relative;
  	left: 0;
  	right: 0;
  	bottom: 0;
  	width: 100%;
  	color: #fff;
}
.footer-col{
   width: 25%;
   padding: 0 15px;
}
.footer-col h4{
	font-size: 18px;
	color: #ffffff;
	text-transform: capitalize;
	margin-bottom: 35px;
	font-weight: 500;
	position: relative;
}
.footer-col h4::before{
	content: '';
	position: absolute;
	left:0;
	bottom: -10px;
	background-color: #e91e63;
	height: 2px;
	box-sizing: border-box;
	width: 50px;
}
.footer-col ul li:not(:last-child){
	margin-bottom: 10px;
}
.footer-col ul li a{
	font-size: 16px;
	text-transform: capitalize;
	color: #ffffff;
	text-decoration: none;
	font-weight: 300;
	color: #bbbbbb;
	display: block;
	transition: all 0.3s ease;
}
.footer-col ul li a:hover{
	color: #ffffff;
	padding-left: 8px;
}
.footer-col .social-links a{
	display: inline-block;
	height: 40px;
	width: 40px;
	background-color: rgba(244, 237, 237, 0.2);
	margin:0 10px 10px 0;
	text-align: center;
	line-height: 40px;
	border-radius: 50%;
	color: #ffffff;
	transition: all 0.5s ease;
}
.footer-col .social-links a:hover{
	color: #725AC1;
	background-color: #ffffff;
}
/*responsive*/
@media(max-width: 767px){
  .footer-col{
    width: 50%;
    margin-bottom: 30px;
}
}
@media(max-width: 574px){
  .footer-col{
    width: 100%;
}
}
</style>