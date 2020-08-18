<template>
  <v-container>
    <v-card class="mx-auto" max-width="800">
      <v-img class="white--text align-end" height="340px" :src="getImage()">
        <v-card-title>{{article.title}}</v-card-title>
      </v-img>

      <v-card-subtitle class="pb-0">{{article.date | dateToDayMonthYear}}</v-card-subtitle>

      <v-card-text class="text--primary">
        <div>{{article.shortDescription}}</div>
      </v-card-text>

      <v-card-actions>
        <v-btn color="orange" text @click="goTo">სრულად</v-btn>
        <v-row
          align="center"
          justify="end"
          style="padding-right:15px"
        >
          <v-icon class="mr-1" color="red lighten-1">mdi-heart</v-icon>
          <span class="subheading mr-2">256</span>
          <v-icon class="mr-1" color="green">mdi-share-variant</v-icon>
          <span class="subheading">45</span>
        </v-row>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script>
import constants from "../constants";

export default {
  name: "Card",
  props: {
    article: {
      title: null
    }
  },

  data: () => ({
     prefixURL: constants.API_PREFIX,
  }),
  created() {
    console.log("in card", this.article);
  },
  methods: {
    getImage(){
      return this.prefixURL+'/' +this.article.imagePath
      // return  "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTEmJIR0yQxjX-R1wJrQn4bL4VLSAZSeA3OS2EIEVxSLYGzntke&usqp=CAU"
    },
    goTo(){
      console.log('this.$router.path',this.$route.path, this.article.id )
      var currentUser = localStorage.getItem("currentUser");
      if(currentUser){
        this.$router.push({ path: 'QuillEditor/' + this.article.id })
        return;
      }
      this.$router.push({ path: this.$route.path +'/' + this.article.id })
    }
  }
};
</script>
