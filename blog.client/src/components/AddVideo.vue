<template>
  <v-container>
    <v-text-field label="ლინკი" v-model="video.url"></v-text-field>
    <v-text-field label="სათაური" v-model="video.title"></v-text-field>
    <v-textarea outlined rows="7" v-model="video.description" label="აღწერა"></v-textarea>

    <v-btn class="ma-2" outlined color="indigo" @click="save">შენახვა</v-btn>
  </v-container>
</template>

<script>
export default {
  name: "AddVideo",
  props: ["id"],
  data: () => ({
    video: {
      url: "",
      title: "",
      description: "",
      dateCreated: null,
    }
  }),
  async created() {
    if (this.id) {
      var result = await this.$http.getVideoById(this.id);
      this.video.url = result.url;
      this.video.title = result.title;
      this.video.description = result.description;
      this.video.dateCreated = result.dateCreated;
      console.log(result);
    }
  },
  methods: {
    async save() {
      if (this.id) {
        var resultUpdate = await this.$http.updateVideo({
          id: Number(this.id),
          ...this.video
        });
        console.log(resultUpdate);
        this.$toast(resultUpdate.message, {
          color: resultUpdate.isSuccess ? "green" : "red",
          dismissable: true
        });
        return;
      }

      var result = await this.$http.addVideo(this.video);
      console.log(result);

      this.$toast(result.message, {
        color: result.isSuccess ? "green" : "red",
        dismissable: true
      });

      if (result.isSuccess) {
        this.video = {
          url: "",
          title: "",
          description: ""
        };
      }
    }
  }
};
</script>
