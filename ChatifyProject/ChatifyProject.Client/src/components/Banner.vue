<template>
    
    <div class="banner-area">
        <div class="font-bg">
                <h1 class="titel">Chatify</h1>
        </div>
        

        <h6 class="untertitel">Wo Kennenlernen leicht wird</h6>

    </div>
</template>


<style scoped>


.banner-area {
    background-image: url(../images/hinterg.jpg) ;
    background-size: cover;
    background-position: center;
    height: 500px;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    text-align: center;
    color: white;
    font-family: 'Roboto', sans-serif;
    font-weight: 100;
    font-size: 20px;
    letter-spacing: 2px;
    text-transform: uppercase;
    padding: 0 20px;
    box-sizing: border-box;
}
.titel {
    font-size: 60px;
    font-weight: bolder;
    background: linear-gradient(to bottom right,#c8c8c9,#ffffff);
    text-align: center;
    -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  padding-top: 20px;


}

.untertitel {
    font-size: 20px;
    font-weight: bolder;
     background: linear-gradient(to bottom right,#7d7d7e,#ffffff);
    text-align: center;
    -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  padding-bottom: 20px;
}

</style>