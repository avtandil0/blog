<template>
  <div>
    <!-- <v-textarea outlined rows="4" label="კომენტარი" @input="updateHTML"></v-textarea> -->
    <quill-editor @input="updateHTML" v-model="data" />
  </div>
</template>

<script>
import { quillEditor } from "vue-quill-editor";

export default {
  name: "HtmlTextarea",
  props: ['text'],
  data: () => ({
    data: null,
  }),
  created(){
    this.data = this.text;
  },
  components: {
    quillEditor
  },
  methods: {
    updateHTML(){
      this.$emit("input", this.data);
    }
  }
};
</script>

