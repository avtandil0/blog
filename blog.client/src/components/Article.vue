<template >
  <div>
    <div align="center" justify="center">
      <!-- <v-btn @click="editArticle" v-if="currentUser" class="ma-2" outlined large fab color="indigo">
        <v-icon>mdi-pencil</v-icon>
      </v-btn>
       <v-btn @click="deleteArticle" v-if="currentUser" class="ma-2" outlined large fab color="red">
        <v-icon>mdi-delete</v-icon>
      </v-btn> -->
      <h1>{{title}}</h1>
    </div>
    <div class="ql-editor" v-html="content"></div>
  </div>
</template>

<script>
export default {
  name: "Article",
  props: ["id"],
  data: () => ({
    content: null,
    title: null,
    currentUser: null
  }),
  async created() {
    this.currentUser = localStorage.getItem("currentUser");
    console.log("currentUser", JSON.parse(this.currentUser));
    this.$store.commit("loadData", true);
    var result = await this.$http.getArticle(this.id);
    console.log(result);
    this.content = result.content;
    this.title = result.title;
    this.$store.commit("loadData", false);
  },
  methods: {
    editArticle(){
      console.log('id', this.id)
    },
    deleteArticle(){
      console.log('id', this.id)
    }
  }
};
</script>
