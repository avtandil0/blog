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
        <v-row align="center" justify="end" style="padding-right:15px">
          <v-icon v-if="isLiked" class="mr-1" color="red lighten-1" @click="downLike(article.id)">mdi-heart</v-icon>
          <v-icon v-else class="mr-1"  @click="setLike(article.id)">mdi-heart-outline</v-icon>
          <span class="subheading mr-2">{{postLikeCount}}</span>

          <!-- <v-icon class="mr-1" color="green">mdi-share-variant</v-icon> -->
          <v-menu offset-y z-index="0">
            <template v-slot:activator="{ on, attrs }">
              <v-icon class="mr-1" color="green" v-bind="attrs" v-on="on">mdi-share-variant</v-icon>
            </template>
            <ShareNetwork
              v-for="network in networks"
              :network="network.network"
              :key="network.network"
              :style="{backgroundColor: network.color}"
              :url="locationPath + '/' + article.id"
              :title="article.title"
            >
              <v-list-item style="background-color: rgb(204,204,204);">
                <v-list-item-content>
                  <v-list-item-title>{{ network.name }}</v-list-item-title>
                </v-list-item-content>
                <v-list-item-icon>
                  <v-icon :style="{color: network.color}">mdi-{{ network.name.toLowerCase() }}</v-icon>
                </v-list-item-icon>
              </v-list-item>
            </ShareNetwork>
          </v-menu>
          <!-- <span class="subheading">45</span> -->
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
      title: null,
    },
  },

  data: () => ({
    prefixURL: constants.API_PREFIX,
    isLiked: false,
    postLikeCount: 0,
    likedArray: [],
    locationPath: null,
    networks: [
      {
        network: "facebook",
        name: "Facebook",
        icon: "fab fah fa-lg fa-facebook-f",
        color: "#1877f2",
      },
      {
        network: "linkedin",
        name: "LinkedIn",
        icon: "fab fah fa-lg fa-linkedin",
        color: "#007bb5",
      },
      {
        network: "pinterest",
        name: "Pinterest",
        icon: "fab fah fa-lg fa-pinterest",
        color: "#bd081c",
      },
      {
        network: "twitter",
        name: "Twitter",
        icon: "fab fah fa-lg fa-twitter",
        color: "#1da1f2",
      },
    ],
  }),
  created() {
    console.log("in card", this.article, window.location.href);
    this.locationPath = window.location.href;
    console.log("------------------", localStorage.getItem("likedArray"));
    this.likedArray = localStorage.getItem("likedArray")? JSON.parse(localStorage.getItem("likedArray")): [];
    this.isLiked = this.likedArray? this.likedArray.filter(r => r == this.article.id).length ? true : false : false

    this.postLikeCount
  },
  watch: {
    isLiked() {
      this.postLikeCount = this.likedArray.filter(r => r == this.article.id).length
    },

  },
  methods: {
    downLike(id){
      this.likedArray = JSON.parse(localStorage.getItem("likedArray"));
      const index = this.likedArray.indexOf(id);
      if (index > -1) {
         this.likedArray.splice(index, 1);
          localStorage.setItem("likedArray", JSON.stringify(this.likedArray));
          this.isLiked = false
      }

    },
    setLike(id) {
      this.likedArray = JSON.parse(localStorage.getItem("likedArray"));
      console.log("id", id,this.likedArray);
      if (this.likedArray && !this.likedArray.filter((r) => r == id).length) {
        this.likedArray.push(id);
        localStorage.setItem("likedArray", JSON.stringify(this.likedArray));
        this.isLiked = true
      }
    },
    getImage() {
      return this.prefixURL + this.article.imagePath;
      // return  "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTEmJIR0yQxjX-R1wJrQn4bL4VLSAZSeA3OS2EIEVxSLYGzntke&usqp=CAU"
    },
    goTo() {
      console.log("this.$router.path", this.$route.path, this.article.id);
      var currentUser = localStorage.getItem("currentUser");
      if (currentUser) {
        this.$router.push({ path: "QuillEditor/" + this.article.id });
        return;
      }
      this.$router.push({ path: this.$route.path + "/" + this.article.id });
    },
  },
};
</script>
